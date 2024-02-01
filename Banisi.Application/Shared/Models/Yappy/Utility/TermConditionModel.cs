using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Utility
{
    public partial class TermConditionResponseModel
    {
        [JsonPropertyName("body")]
        public Body Body { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public partial class Body
    {
        [JsonPropertyName("terms_conditions")]
        public TermsCondition[] TermsConditions { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public partial class TermsCondition
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("details")]
        public StatementElement[] Details { get; set; }

        [JsonPropertyName("subtitle")]
        public Subtitle[] Subtitle { get; set; }
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
        public SubtitleDetail[] Details { get; set; }
    }

    public partial class SubtitleDetail
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("statements")]
        public StatementElement[] Statements { get; set; }
    }

    public partial class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
