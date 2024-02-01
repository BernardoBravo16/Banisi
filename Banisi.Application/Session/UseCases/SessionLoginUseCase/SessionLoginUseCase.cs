using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Banisi.Application.Session.UseCases.SessionLoginUseCase
{
    public class SessionLoginUseCase : ISessionLoginUseCase
    {
        private readonly GeneralSettings _generalSettings;
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;

        private const string STATUS_CODE = "YP-0000";

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public SessionLoginUseCase(IApiClientService apiClientService, IOptions<GeneralSettings> generalSettings)
        {
            _apiClientService = apiClientService;
            _generalSettings = generalSettings.Value;
        }

        public async Task Execute(LoginSessionRequest request)
        {
            string affiliationId = request.AffiliationId.ToString();
            string hashedValue = GenerateSha256Hash(affiliationId, request.Seed);

            request.Body.UniqueId = request.AffiliationId;
            request.Body.Code = hashedValue;

            LoginSessinResponse response = await PostSessionLogin(request);

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
                Data = response,
                Status = new Status
                {
                    Code = "200",
                    Description = "Operación exitosa"
                }
            };

            _outputPort.Ok(succesResponse);
            return;
        }

        private string GenerateSha256Hash(string affiliationId, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la entrada y el salt en un array de bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(affiliationId);
                byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

                // Crear un array de bytes para almacenar la combinación de la entrada y el salt
                byte[] inputWithSaltBytes = new byte[inputBytes.Length + saltBytes.Length];

                // Copiar los bytes de la entrada y el salt al array de la combinación
                Buffer.BlockCopy(inputBytes, 0, inputWithSaltBytes, 0, inputBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, inputWithSaltBytes, inputBytes.Length, saltBytes.Length);

                // Calcular el hash de la combinación de la entrada y el salt
                byte[] hashBytes = sha256Hash.ComputeHash(inputWithSaltBytes);

                // Convertir el hash en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                // Retornar el hash en formato de cadena hexadecimal
                return builder.ToString();
            }
        }
       
        private StringContent GenerateBodyRequest(LoginSessionRequest model)
        {
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

            var body = new BodySession
            {
                Kyc = kyc
            };

            var json = JsonConvert.SerializeObject(body);
            var personalContent = new StringContent(json, Encoding.UTF8, "application/json");

            return personalContent;
        }

        private async Task<LoginSessinResponse> PostSessionLogin(LoginSessionRequest request)
        {
            string appName = _generalSettings.Yappy_AppName;
            string appApiKey = _generalSettings.Yappy_Api_Key;
            string appSecretKey = _generalSettings.Yappy_Secrect_Key;

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var clientPost = new HttpClient(handler);

            var headers = new Dictionary<string, string>
            {
                { "App-Version", request.App_Version },
                { "App-Name", appName },
                { "Client-Ip", request.Client_Ip },
                { "App-Api-Key", appApiKey },
                { "App-Secret-Key", appSecretKey }
            };

            string urlYappy = _generalSettings.Yappy_Url;

            string url = $"{urlYappy}session/login";

            StringContent requestContent = GenerateBodyRequest(request);

            var personalRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = requestContent
            };

            var responseSession = await clientPost.SendAsync(personalRequest);
            responseSession.EnsureSuccessStatusCode();

            var responseContent = await responseSession.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            LoginSessinResponse response = System.Text.Json.JsonSerializer.Deserialize<LoginSessinResponse>(responseContent, options);

            return response;
        }
    }
}