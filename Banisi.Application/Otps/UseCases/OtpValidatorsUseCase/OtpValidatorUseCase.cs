using Banisi.Application.ModelsYappy;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;

namespace Banisi.Application.Otps.UseCases.OtpValidatorsUseCase
{
    public class OtpValidatorUseCase : IOtpValidatorUseCase
    {
        private IOutputPort _outputPort;
        private readonly GeneralSettings _generalSettings;

        private const string CANAL = "BMOVIL";
        private const int CODESERVICE = 19;

        public OtpValidatorUseCase(IOptions<GeneralSettings> generalSettings)
        {
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(OtpValidatorModel model)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            ResponseOtp responseOtp = new ResponseOtp();

            using (var client = new HttpClient(handler))
            {
                var urlBanisi = _generalSettings.Banisi_Url;

                var request = new HttpRequestMessage(HttpMethod.Post, $"{urlBanisi}Transacciones/ValidarOTP");

                var userName = _generalSettings.Banisi_UserName;
                var password = _generalSettings.Banisi_Password;

                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));

                request.Headers.Add("Authorization", $"Basic {token}");

                var content =
                    new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        CodigoCliente = model.ClientId,
                        Canal = CANAL,
                        CodigoCanal = CODESERVICE,
                        DireccionIP = model.ClientIp,
                        OTP = model.Otp
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

                responseOtp = System.Text.Json.JsonSerializer.Deserialize<ResponseOtp>(responseContent, options);
            }

            _outputPort.Ok(responseOtp);
        }
    }
}