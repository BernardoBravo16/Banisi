using System.Text.Json.Serialization;

namespace Banisi.Application.Utilitys.UseCases.GetTermsConditions
{
    public partial class TermsConditionsResponse
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("body")]
        public Body Body { get; set; }
    }

    public partial class Body
    {
        [JsonPropertyName("terms_conditions")]
        public List<TermsCondition> TermsConditions { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public partial class TermsCondition
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("details")]
        public List<StatementElement> Details { get; set; }

        [JsonPropertyName("subtitle")]
        public List<Subtitle> Subtitle { get; set; }
    }

    public partial class StatementElement
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public partial class Subtitle
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("details")]
        public List<SubtitleDetail> Details { get; set; }
    }

    public partial class SubtitleDetail
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("statements")]
        public List<StatementElement> Statements { get; set; }
    }

    public partial class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
