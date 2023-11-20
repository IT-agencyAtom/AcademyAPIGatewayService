using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class ClientRoot
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public ClientOutput Data { get; set; }
    }
}
