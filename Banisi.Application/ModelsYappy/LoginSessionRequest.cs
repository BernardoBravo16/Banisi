using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class LoginSessionRequest
    {
        public Guid AffiliationId { get; set; }
        public string Seed { get; set; }
        public string App_Version { get; set; }
        public string Client_Ip { get; set; }

        [JsonPropertyName("body")]
        public BodySession Body { get; set; }
    }

    public partial class BodySession
    {

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("unique_id")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("kyc")]
        public Kyc Kyc { get; set; }
    }
}