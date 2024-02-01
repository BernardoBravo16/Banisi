using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Common.Configuration;
using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Banisi.Application.Shared.Resources;

namespace Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase
{
    public class GetCheckAffiliationUseCase : IGetCheckAffiliationUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;

        private readonly GeneralSettings _generalSettings;

        private const int CANALCODE = 2;
        private const int CODESERVICE = 28001;

        public GetCheckAffiliationUseCase(IRepository<Affiliation, Guid> repository, IApiClientService apiClientService, IOptions<GeneralSettings> generalSettings)
        {
            _repository = repository;
            _apiClientService = apiClientService;
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(int clientId)
        {
            var affiliation = await _repository.GetAll()
                .Where(a => a.ClientId == clientId).FirstOrDefaultAsync();

            Client associatedClient = null;

            //Si el cliente se encuentra afiliado enviar directo al dashboard!!

            if (affiliation == null)
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var client = new HttpClient(handler))
                {
                    var urlBanisi = _generalSettings.Banisi_Url;

                    var request = new HttpRequestMessage(HttpMethod.Post, $"{urlBanisi}consultas/cliente");

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
                            DireccionIP = "192.168.1.13"
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

                    associatedClient = System.Text.Json.JsonSerializer.Deserialize<Client>(responseContent, options);

                    if (associatedClient.ClientCode == 0)
                    {
                        var errorResponse = new ApiResponse
                        {
                            Data = null,
                            Status = new Status
                            {
                                Code = "400",
                                Description = VerificationsResources.CustomerNotAffilated
                            }                            
                        };

                        _outputPort.Ok(errorResponse);
                        return;
                    }
                }
            }

            var result = new AffiliationResult(affiliation, associatedClient);

            var succesResponse = new ApiResponse
            {
                Data = result,
                Status = new Status
                {
                    Code = "200",
                    Description = "Operación exitosa"
                }
            };

            _outputPort.Ok(succesResponse);
        }
    }
}