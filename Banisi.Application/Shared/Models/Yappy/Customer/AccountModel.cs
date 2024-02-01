using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Account
{
    public class CustomerModel
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("identification_type")]
        public string IdentificationType { get; set; }

        [JsonPropertyName("accounts")]
        public List<AccountModel> Accounts { get; set; }
    }

    public class AccountModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("mask")]
        public string Mask { get; set; }

        [JsonPropertyName("balance")]
        public double Balance { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public class AccountResponseModel
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
