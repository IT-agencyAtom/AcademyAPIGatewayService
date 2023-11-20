using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CreateCatalogElementsInput
    {
        [JsonIgnore]
        public long CatalogId { get; set; }

        [JsonIgnore]
        public IList<CreateCatalogElementsListItem> Items { get; set; }
    }

    public class CreateCatalogElementsListItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public List<CreateCatalogElementsCustomFieldsValue> CustomFieldsValues { get; set; }
    }

    public class CreateCatalogElementsCustomFieldsValue
    {
        [JsonPropertyName("field_id")]
        public long FieldId { get; set; }

        [JsonPropertyName("values")]
        public List<CreateCatalogElementsValue> Values { get; set; }
    }

    public class CreateCatalogElementsValue
    {
        [JsonPropertyName("enum_id")]
        public long? EnumId { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}
