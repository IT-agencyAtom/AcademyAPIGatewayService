using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class Lead
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
