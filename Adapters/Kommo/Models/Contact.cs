using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class Contact
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }
    }
}
