using System.Text.Json.Serialization;

namespace Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias
{
    public class AvaliabilityAliasResponse
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public partial class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
