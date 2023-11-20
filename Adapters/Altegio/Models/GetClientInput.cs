using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class GetClientInput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonIgnore]
        public long CompanyId { get; set; }
    }
}
