using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class LeadEmbedded
    {
        [JsonPropertyName("contacts")]
        public List<LeadContact> Contacts { get; set; }
    }
}
