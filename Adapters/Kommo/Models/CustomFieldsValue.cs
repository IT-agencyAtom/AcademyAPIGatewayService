using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CustomFieldsValue
    {
        [JsonPropertyName("field_id")]
        public int FieldId { get; set; }

        [JsonPropertyName("field_name")]
        public string FieldName { get; set; }

        [JsonPropertyName("field_code")]
        public string FieldCode { get; set; }

        [JsonPropertyName("field_type")]
        public string FieldType { get; set; }

        [JsonPropertyName("values")]
        public List<CustomFieldValue> Values { get; set; }
    }
}
