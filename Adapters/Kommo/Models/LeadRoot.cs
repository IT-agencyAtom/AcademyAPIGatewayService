using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class LeadsEmbedded
    {
        [JsonPropertyName("leads")]
        public List<LeadOutput> Leads { get; set; }
    }

    public class LeadRoot
    {
        [JsonPropertyName("_page")]
        public int Page { get; set; }

        [JsonPropertyName("_embedded")]
        public LeadsEmbedded Embedded { get; set; }
    }
}
