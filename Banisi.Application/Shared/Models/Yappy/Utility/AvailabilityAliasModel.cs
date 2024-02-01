using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Utility
{
    public class AvailabilityAliasResponseModel
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
