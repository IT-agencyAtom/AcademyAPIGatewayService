using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class GetClientsInput
    {
        [JsonIgnore]
        public long CompanyId { get; set; }
        [JsonPropertyName("fields")]
        public string[] Fields { get; set; }
        [JsonPropertyName("operation")]
        public string Operation { get; set; }
        public IList<ClientFilter> Filters { get; set; }
    }

    public class ClientFilter
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("state")]
        public ClientFilterState State { get; set; }
    }

    public class ClientFilterState
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
