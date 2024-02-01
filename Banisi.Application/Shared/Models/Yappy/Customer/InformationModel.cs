using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Customer
{
    public class InformationResponseModel
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("identification_type")]
        public string IdentificationType { get; set; }

        [JsonPropertyName("birth_date")]
        public DateTimeOffset BirthDate { get; set; }

        [JsonPropertyName("place_birth")]
        public string PlaceBirth { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
