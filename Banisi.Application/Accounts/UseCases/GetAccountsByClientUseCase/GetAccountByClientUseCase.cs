using Banisi.Application.ModelsYappy;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;

namespace Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase
{
    public class GetAccountByClientUseCase : IGetAccountByClientUseCase
    {
        private readonly GeneralSettings _generalSettings;
        private IOutputPort _outputPort;

        private const int CANALCODE = 2;
        private const int CODESERVICE = 20002;

        public GetAccountByClientUseCase(IOptions<GeneralSettings> generalSettings)
        {
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(int clientId, string clientIp)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            RootObject rootObject = new RootObject();

            using (var client = new HttpClient(handler))
            {
                var urlBanisi = _generalSettings.Banisi_Url;

                var request = new HttpRequestMessage(HttpMethod.Post, $"{urlBanisi}consultas/cliente-cuentas");

                var userName = _generalSettings.Banisi_UserName;
                var password = _generalSettings.Banisi_Password;

                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));

                request.Headers.Add("Authorization", $"Basic {token}");

                var content =
                    new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        CodigoCanal = CANALCODE,
                        CodigoServicio = CODESERVICE,
                        CodigoCliente = clientId,
                        DireccionIP = clientIp
                    }),
                    Encoding.UTF8, "application/json");

                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(responseContent, options);
            }

            _outputPort.Ok(rootObject);
        }
    }
}