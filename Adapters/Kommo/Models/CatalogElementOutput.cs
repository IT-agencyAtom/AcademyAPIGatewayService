using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CatalogElementOutput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("created_by")]
        public int CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public int UpdatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public object IsDeleted { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public List<CustomFieldsValue> CustomFieldsValues { get; set; }

        [JsonPropertyName("catalog_id")]
        public int CatalogId { get; set; }

        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }
    }
}
