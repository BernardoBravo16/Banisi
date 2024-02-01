using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Authorization
{
    public class TokenModel
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }

        [JsonPropertyName("client_id")]
        public Guid ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public Guid ClientSecret { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }

    public class TokenResponseModel
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("consented_on")]
        public long ConsentedOn { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expires_in")]
        public long RefreshTokenExpiresIn { get; set; }

        [JsonPropertyName("id_token")]
        public string TokenId { get; set; }
    }
}
