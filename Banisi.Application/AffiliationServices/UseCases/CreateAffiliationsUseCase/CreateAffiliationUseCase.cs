using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Banisi.Application.ModelsYappy;
using Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Application.Shared.Resources;
using Banisi.Common.Configuration;
using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase
{
    public class CreateAffiliationUseCase : ICreateAffiliationUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private readonly ICreatePersonProfileUseCase _createPersonProfileUseCase;
        private readonly GeneralSettings _generalSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAmazonCognitoIdentityProvider _cognitoService;
        private IOutputPort _outputPort;

        private const string STATUS_CODE = "YP-0000";
        private const string APP_NAME = "YAPPY_BM_BS";
        private const string CHARS = "abcdefghijklmnopqrstuvwxyz";
        private const string NUMS = "0123456789";
        private const string SYMS = "!@#$%^&*";
        private static Random random = new Random();

        public CreateAffiliationUseCase(IRepository<Affiliation, Guid> repository, IUnitOfWork unitOfWork, ICreatePersonProfileUseCase createPersonProfileUseCase, IOptions<GeneralSettings> generalSettings, IAmazonCognitoIdentityProvider cognitoService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _createPersonProfileUseCase = createPersonProfileUseCase;
            _generalSettings = generalSettings.Value;
            _cognitoService = cognitoService;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(ClientAffiliationRequest model)
        {
            var getAffiliated = await _repository.GetAll().Where(a => a.ClientId == model.ClientId).FirstOrDefaultAsync();

            if (getAffiliated != null)
            {
                var errorResponse = new ApiResponse
                {
                    Data = null,
                    Status = new Status
                    {
                        Code = "400",
                        Description = VerificationsResources.ClientIsAffiliatedWithYappy
                    }
                };

                _outputPort.Ok(errorResponse);
                return;
            }

            try
            {
                var userAttrs = new AttributeType
                {
                    Name = "preferred_username",
                    Value = "test",
                };

                var userAttrsList = new List<AttributeType>();

                userAttrsList.Add(userAttrs);

                StringBuilder password = GeneratePassword();

                var username = $"YAPPY-{model.ClientId}";

                var signUpRequest = new SignUpRequest
                {
                    UserAttributes = userAttrsList,
                    Username = username,
                    ClientId = _generalSettings.AWS_ClientId,
                    Password = password.ToString()
                };

                var responseCognito = await _cognitoService.SignUpAsync(signUpRequest);

                var confirm = await _cognitoService.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest
                {
                    Username = username,
                    UserPoolId = _generalSettings.AWS_UserPool,

                });

                var affiliated = new Affiliation
                {
                    Id = Guid.Parse(responseCognito.UserSub),
                    ClientId = model.ClientId,
                    AccountId = model.AccountId,
                    Otp = Guid.NewGuid(),
                    CognitoUsername = username,
                    CognitoPassword = password.ToString(),
                    CreateDate = DateTime.Now,
                };

                await _repository.AddAsync(affiliated);

                await _unitOfWork.SaveAsync();

                ClientAffiliationRequest profileModel = new ClientAffiliationRequest();

                profileModel.Body = model.Body;
                profileModel.Body.UniqueId = affiliated.Id;
                profileModel.Body.Otp = affiliated.Otp;

                StatusResponse response = await PostPersonProfile(model.App_Version, model.Client_Ip, model);

                if (response.Status.Code != STATUS_CODE)
                {
                    var errorResponse = new ApiResponse
                    {
                        Data = null,
                        Status = new Status
                        {
                            Code = "400",
                            Description = response.Status.Description
                        }
                    };

                    _outputPort.Ok(errorResponse);
                    return;
                }

                var succesResponse = new ApiResponse
                {
                    Data = affiliated,
                    Status = new Status
                    {
                        Code = "200",
                        Description = "Operación exitosa"
                    }
                };

                _outputPort.Ok(succesResponse);
                return;
            }
            catch (Exception error)
            {
                var errorResponse = new ApiResponse();

                switch (error)
                {
                    case UsernameExistsException:
                        errorResponse = new ApiResponse
                        {
                            Data = null,
                            Status = new Status
                            {
                                Code = "400",
                                Description = "El usuario ya se encuentra registrado."
                            }
                        };
                        break;
                    case NotAuthorizedException:
                        errorResponse = new ApiResponse
                        {
                            Data = null,
                            Status = new Status
                            {
                                Code = "400",
                                Description = "El usuario no se pudo registrar faltan datos."
                            }
                        };
                        break;

                }

                _outputPort.Ok(errorResponse);
                return;
            }


        }

        private async Task<StatusResponse> PostPersonProfile(string appVersion, string clientIp, ClientAffiliationRequest model)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var clientPost = new HttpClient(handler);

            var headers = new Dictionary<string, string>
            {
                { "App-Version", appVersion },
                { "App-Name", APP_NAME },
                { "Client-Ip", clientIp },
            };

            string urlYappy = _generalSettings.Yappy_Url;

            string url = $"{urlYappy}personal/profile";

            StringContent requestContent = GenerateBodyRequest(model);

            var personalRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = requestContent
            };

            string appApiKey = _generalSettings.Yappy_Api_Key;
            string appSecretKey = _generalSettings.Yappy_Secrect_Key;

            personalRequest.Headers.Add("App-Api-Key", appApiKey);
            personalRequest.Headers.Add("App-Secret-Key", appSecretKey);

            foreach (var header in headers)
            {
                personalRequest.Headers.Add(header.Key, header.Value);
            }

            var responsePersonal = await clientPost.SendAsync(personalRequest);
            responsePersonal.EnsureSuccessStatusCode();

            var responseContent = await responsePersonal.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            StatusResponse response = System.Text.Json.JsonSerializer.Deserialize<StatusResponse>(responseContent, options);

            return response;
        }

        private StringContent GenerateBodyRequest(ClientAffiliationRequest model)
        {
            var termsConditions = new ModelsYappy.TermsConditions
            {
                Accepted = true,
                Version = model.Body.TermsConditions.Version
            };

            var fingerprint = new ModelsYappy.Fingerprint
            {
                HardwareId = model.Body.Kyc.Fingerprint.HardwareId,
                DeviceModel = model.Body.Kyc.Fingerprint.DeviceModel,
                DeviceName = model.Body.Kyc.Fingerprint.DeviceName,
                OsName = model.Body.Kyc.Fingerprint.OsName,
                Version = model.Body.Kyc.Fingerprint.Version,
                Language = model.Body.Kyc.Fingerprint.Language,
                WifiSsid = model.Body.Kyc.Fingerprint.WifiSsid,
                LocalizationAreaId = model.Body.Kyc.Fingerprint.LocalizationAreaId,
                ScreenSize = model.Body.Kyc.Fingerprint.ScreenSize,
                TimeStamp = model.Body.Kyc.Fingerprint.TimeStamp,
                Compromised = model.Body.Kyc.Fingerprint.Compromised,
                Emulator = model.Body.Kyc.Fingerprint.Emulator,
                RequestedIp = model.Body.Kyc.Fingerprint.RequestedIp
            };

            var geolocation = new ModelsYappy.Geolocation
            {
                Latitude = model.Body.Kyc.Geolocation.Latitude,
                Longitude = model.Body.Kyc.Geolocation.Longitude
            };

            var kyc = new ModelsYappy.Kyc
            {
                Fingerprint = fingerprint,
                Geolocation = geolocation
            };

            var body = new ModelsYappy.Body
            {
                Alias = model.Body.Alias,
                Otp = model.Body.Otp,
                UniqueId = Guid.NewGuid(),
                TermsConditions = termsConditions,
                Kyc = kyc
            };

            var json = JsonConvert.SerializeObject(body);
            var personalContent = new StringContent(json, Encoding.UTF8, "application/json");

            return personalContent;
        }

        private StringBuilder GeneratePassword()
        {
            StringBuilder password = new StringBuilder();

            password.Append(CHARS[random.Next(CHARS.Length)]);
            password.Append(CHARS.ToUpper()[random.Next(NUMS.Length)]);
            password.Append(NUMS[random.Next(NUMS.Length)]);
            password.Append(SYMS[random.Next(SYMS.Length)]);

            string allchars = CHARS + CHARS.ToUpper() + NUMS + SYMS;

            for (int i = 4; i < 10; i++)
            {
                password.Append(allchars[random.Next(allchars.Length)]);
            }

            return password;
        }
    }
}