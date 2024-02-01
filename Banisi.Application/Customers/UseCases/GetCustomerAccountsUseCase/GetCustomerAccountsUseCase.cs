using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Application.Shared.Resources;
using Banisi.Common.Configuration;
using Banisi.Domain.Entities.Affiliations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase
{
    public class GetCustomerAccountsUseCase : IGetCustomerAccountsUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private readonly GeneralSettings _generalSettings;
        private IOutputPort _outputPort;

        private const int CANALCODE = 2;
        private const int CODESERVICE = 28001;
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); // Clave AES de 16 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // IV AES de 16 bytes


        public GetCustomerAccountsUseCase(IOptions<GeneralSettings> generalSettings, IRepository<Affiliation, Guid> repository)
        {
            _generalSettings = generalSettings.Value;
            _repository = repository;
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

            Account account = await GetAccount(getAffiliation.AccountId);

            account.FormattedAccount = account.FormattedAccount.Replace("-", "");

            int visibleDigits = 4;

            string mask = new string('*', Math.Max(0, account.FormattedAccount.Length - visibleDigits)) +
                 account.FormattedAccount.Substring(Math.Max(0, account.FormattedAccount.Length - visibleDigits));

            string prueba3 = EncryptLong(getAffiliation.AccountId);


            var customerAccount = new CustomerAccountModel
            {
                FirstName = firstName,
                LastName = lastName,
                Identification = client.Identification,
                IdentificationType = MapIdentificationTypeToEnum(client.IdentificationType),
                Account = new dataAccount
                {
                    Id = EncryptLong(getAffiliation.AccountId),
                    Type = account.ApplicationCode == "bca" ? "Saving" : "Current",
                    Mask = mask,
                    Balance = account.Value1,
                    Currency = "USD",
                    UpdateAt = account.OpeningDate
                }
            };

            var successResponse = new ApiResponse
            {
                Data = customerAccount,
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

        private async Task<Account> GetAccount(long accountId)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var client = new HttpClient(handler);

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
                    CodigoCanal = 6,
                    CodigoServicio = 60010,
                    CuentaCompleta = accountId.ToString(),
                    DireccionIP = "1.1.1.1"
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

            var rootObject = JsonSerializer.Deserialize<RootObject>(responseContent, options);          
            
            var account = rootObject.Accounts.FirstOrDefault();

            return account;
        }

        private static string MapIdentificationTypeToEnum(string identificationType)
        {
            var cedulaTypes = new HashSet<string> { "CÉDULA ECUADOR", "CÉDULA PANAMEÑA", "PANAMEÑO INDIGENA" };

            // Compara, ignorando mayúsculas y minúsculas, si es alguno de los tipos de cédula
            if (cedulaTypes.Contains(identificationType))
            {
                return "CED";
            }
            else
            {
                // Si no es ninguno de los anteriores, por defecto es Passport
                return "PAS";
            }
        }

        public static string EncryptLong(long number)
        {
            // Convierte el número long a string para cifrarlo
            string plainText = number.ToString();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
    }
}