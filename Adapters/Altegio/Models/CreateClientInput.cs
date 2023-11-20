using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class CreateClientInput
    {
        [JsonIgnore]
        public long CompanyId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
