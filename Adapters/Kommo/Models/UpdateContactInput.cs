using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class UpdateContactCustomFieldsValue
    {
        [JsonPropertyName("field_id")]
        public long FieldId { get; set; }
        [JsonPropertyName("values")]
        public List<UpdateContactCustomFieldValue> Values { get; set; }
    }

    public class UpdateContactCustomFieldValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class UpdateContactInput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("custom_fields_values")]
        public IList<UpdateContactCustomFieldsValue> CustomFieldsValues { get; set; }
    }
}
