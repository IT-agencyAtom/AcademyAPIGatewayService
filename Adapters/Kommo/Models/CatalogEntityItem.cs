using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CatalogEntityItem
    {
        [JsonPropertyName("entity_type")]
        public string EntityType { get; set; }
        [JsonPropertyName("entity_id")]
        public long EntityId { get; set; }
    }
}
