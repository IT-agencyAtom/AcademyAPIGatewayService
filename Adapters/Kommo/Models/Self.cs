using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class Self
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}
