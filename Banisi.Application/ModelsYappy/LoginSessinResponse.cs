using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class LoginSessinResponse
    {
        [JsonPropertyName("body")]
        public BodySessionResponse Body { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public class BodySessionResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
