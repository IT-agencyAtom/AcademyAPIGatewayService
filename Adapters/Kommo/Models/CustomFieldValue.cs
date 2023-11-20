using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CustomFieldValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("enum_id")]
        public int? EnumId { get; set; }

        [JsonPropertyName("enum_code")]
        public string EnumCode { get; set; }
    }
}
