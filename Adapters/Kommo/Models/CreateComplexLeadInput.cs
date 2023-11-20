using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class ComplexLeadContact
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public List<ComplexLeadCustomFieldsValue> CustomFieldsValues { get; set; }
    }

    public class ComplexLeadCustomFieldsValue
    {
        [JsonPropertyName("field_code")]
        public string FieldCode { get; set; }

        [JsonPropertyName("field_name")]
        public string FieldName { get; set; }

        [JsonPropertyName("values")]
        public List<ComplexLeadValue> Values { get; set; }
    }

    public class ComplexLeadEmbedded
    {
        [JsonPropertyName("contacts")]
        public List<ComplexLeadContact> Contacts { get; set; }
    }

    public class CreateComplexLeadInput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("_embedded")]
        public ComplexLeadEmbedded Embedded { get; set; }

        [JsonPropertyName("status_id")]
        public long StatusId { get; set; }

        [JsonPropertyName("pipeline_id")]
        public long PipelineId { get; set; }
    }

    public class ComplexLeadValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("enum_code")]
        public string EnumCode { get; set; }
    }
}
