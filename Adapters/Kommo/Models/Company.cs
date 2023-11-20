using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class Company
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }
    }
}
