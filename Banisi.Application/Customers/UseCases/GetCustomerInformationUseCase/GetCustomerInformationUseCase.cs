using Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Application.Shared.Resources;
using Banisi.Common.Configuration;
using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase
{
    public class GetCustomerInformationUseCase : IGetCustomerInformationUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private readonly GeneralSettings _generalSettings;
        private IOutputPort _outputPort;

        private const int CANALCODE = 2;
        private const int CODESERVICE = 28001;

        public GetCustomerInformationUseCase(IRepository<Affiliation, Guid> repository, IOptions<GeneralSettings> generalSettings)
        {
            _repository = repository;
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
        public async Task Execute()
        {
            var id = "F10A072D-93F5-46A8-B23A-294EDD9A13E1";

            var getAffiliation = await _repository.GetAll()
                .Where(a => a.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            if (getAffiliation == null)
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

            Client client = await GetClient(getAffiliation.ClientId);

            string[] nameParts = client.FullName.Split(' ');

            string firstName = nameParts.ElementAtOrDefault(0) ?? "";
            string lastName = nameParts.ElementAtOrDefault(2) ?? "";


            var customerInformation = new CustomerInformationModel
            {
                FirstName = firstName,
                LastName = lastName,
                Identification = client.Identification,
                IdentificationType = MapIdentificationTypeToEnum(client.IdentificationType),
                Birthdate = client.BirthDate,
                Email = client.Email,
                PlaceBirth = client.PlaceBirth
            };

            var successResponse = new ApiResponse
            {
                Data = customerInformation,
                Status = new Status
                {
                    Code = "200",
                    Description = "Operación exitosa."
                }
            };

            _outputPort.Ok(successResponse);
            return;
        }

        private async Task<Client> GetClient(int clientId)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var client = new HttpClient(handler);

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

            Client getClient = JsonSerializer.Deserialize<Client>(responseContent, options);

            return getClient;
        }

        private static string MapIdentificationTypeToEnum(string identificationType)
        {
            var cedulaTypes = new HashSet<string> { "cedula ecuador", "cedula panameña", "extranjero indigena" };

            // Compara, ignorando mayúsculas y minúsculas, si es alguno de los tipos de cédula
            if (cedulaTypes.Contains(identificationType.ToLowerInvariant()))
            {
                return "CED";
            }
            else
            {
                // Si no es ninguno de los anteriores, por defecto es Passport
                return "PAS";
            }
        }
    }
}