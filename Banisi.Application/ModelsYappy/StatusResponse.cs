using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class StatusResponse
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
