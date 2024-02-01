
using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class OauthTokenRequest
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("grant_type")]
        public string GrandType { get; set; }

        [JsonPropertyName("client_id")]
        public Guid ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public Guid ClientSecret { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
