using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Application.Shared.Models.Base;
using Banisi.Application.Shared.Models.Yappy.Personal;
using Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase
{
    public class CreatePersonProfileUseCase : ICreatePersonProfileUseCase
    {
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;
        private readonly GeneralSettings _generalSettings;

        private const string APP_NAME = "YAPPY_BM_BS";
        private const string STATUS_CODE = "YP-0000";

        public CreatePersonProfileUseCase(IApiClientService apiClientService, IOptions<GeneralSettings> generalSettings)
        {
            _apiClientService = apiClientService;
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(string appVersion, string clientIp, ClientAffiliationRequest model)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var clientPost = new HttpClient(handler);

            var headers = new Dictionary<string, string>
            {
                { "App-Version", appVersion },
                { "App-Name", APP_NAME },
                { "Client-Ip", clientIp }
            };

            string url = "https://api-mobile-integration-qa.yappycloud.com/v1/personal/profile";

            StringContent requestContent = GenerateBodyRequest(model);

            var personalRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = requestContent
            };

            foreach (var header in headers)
            {
                personalRequest.Headers.Add(header.Key, header.Value);
            }

            var responsePersonal = await clientPost.SendAsync(personalRequest);
            responsePersonal.EnsureSuccessStatusCode();

            var responseContent = await responsePersonal.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            StatusResponse response = System.Text.Json.JsonSerializer.Deserialize<StatusResponse>(responseContent, options);

            if (response.Status.Code == STATUS_CODE)
            {
                string clientId = _generalSettings.AWS_AccessKey; // Este valor debería ser proporcionado por la configuración o modelo
                string codeChallenge = ""; // Este valor debería ser generado por la aplicación
                string otp = ""; // Este valor es específico de la solicitud
                string loginHint = ""; // Este valor identifica al usuario que está autorizando la aplicación
                string scope = "openid"; // Scope fijo para este ejemplo
                string responseType = "code"; // Response type fijo para este ejemplo
                string redirectUri = "https://oauth.yappycloud.com/add"; // Tu URI de redireccionamiento registrado
                bool showScreen = true; // Si quieres mostrar la pantalla o no

                string baseUrl = "https://api-mobile-integration-qa.yappycloud.com/v1";

                url = $"{baseUrl}/oauth2/authorize?scope={Uri.EscapeDataString(scope)}" +
                           $"&response_type={Uri.EscapeDataString(responseType)}" +
                           $"&client_id={Uri.EscapeDataString(clientId)}" +
                           $"&code_challenge={Uri.EscapeDataString(codeChallenge)}" +
                           $"&otp={Uri.EscapeDataString(otp)}" +
                           $"&show_screen={showScreen.ToString().ToLowerInvariant()}" +
                           $"&login_hint={Uri.EscapeDataString(loginHint)}" +
                           $"&redirect_uri={Uri.EscapeDataString(redirectUri)}";

                string username = model.Body.UniqueId.ToString();
                string password = model.Body.Otp.ToString();

                string encodedCredentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));

                headers = new Dictionary<string, string>
                {
                    { "Authorization", $"Basic {encodedCredentials}" }
                };

                // client HTTP
                var client = new HttpClient();

                var getRequest = new HttpRequestMessage(HttpMethod.Get, url);
                getRequest.Headers.Add("Authorization", $"Basic {encodedCredentials}");

                // Realizar la solicitud GET y obtener la respuesta
                var getResponse = await client.SendAsync(getRequest);

                getResponse.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta es exitosa

                var getResponseContent = await getResponse.Content.ReadAsStringAsync();


                if (getResponse.StatusCode == HttpStatusCode.Redirect ||
                    getResponse.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    // Obtener la URL de la redirección desde la cabecera 'Location'
                    var locationHeader = getResponse.Headers.Location;

                    if (locationHeader != null)
                    {
                        // Extraer el código de autorización de la URL
                        var queryParameters = System.Web.HttpUtility.ParseQueryString(locationHeader.Query);
                        string authorizationCode = queryParameters["code="];

                        handler = new HttpClientHandler();
                        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                        using (var clientPostToken = new HttpClient(handler))
                        {
                            url = "https://api-mobile-integration-qa.yappycloud.com/v1/oauth2/token";

                            var request = new HttpRequestMessage(HttpMethod.Post, url);

                            var content = new StringContent(
                                         JsonConvert.SerializeObject(new
                                         {
                                             code = authorizationCode,
                                             grand_type = "authorization_code",
                                             client_id = _generalSettings.AWS_AccessKey,
                                             client_secret = _generalSettings.AWS_SecretKey,
                                             scope = "openid"
                                         }),
                                         Encoding.UTF8, "application/json");

                            request.Content = content;

                            var responseToken = await clientPostToken.SendAsync(request);
                            responseToken.EnsureSuccessStatusCode();

                            responseContent = await responseToken.Content.ReadAsStringAsync();

                            options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            OauthTokenResponse responseOtp = System.Text.Json.JsonSerializer.Deserialize<OauthTokenResponse>(responseContent, options);
                        }
                    }
                }
            }

            _outputPort.Ok(response);
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
            };

            var geolocation = new ModelsYappy.Geolocation
            {
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
    }
}