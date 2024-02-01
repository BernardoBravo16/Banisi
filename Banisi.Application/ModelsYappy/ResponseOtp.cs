using System.Text.Json.Serialization;
namespace Banisi.Application.ModelsYappy
{
    public class ResponseOtp
    {
        [JsonPropertyName("id_error")]
        public int Id { get; set; }

        [JsonPropertyName("descripcion_error")]
        public string Description { get; set; }
    }
}
