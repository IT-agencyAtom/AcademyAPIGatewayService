using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class Links
    {
        [JsonPropertyName("self")]
        public Self Self { get; set; }
    }
}
