using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Customer
{
    public class SeedModel
    {
        [JsonPropertyName("seed")]
        public string Seed { get; set; }
    }

    public class SeedResponseModel
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}