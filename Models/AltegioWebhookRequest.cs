using System.Text.Json.Serialization;

namespace CrmIntegration.Models
{
    public class Client
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

    public class Data
    {
        [JsonPropertyName("client")]
        public Client Client { get; set; }
        [JsonPropertyName("services")]
        public List<Service> Services { get; set; }
    }

    public class AltegioWebhookRequest
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Service
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("cost")]
        public int Cost { get; set; }

        [JsonPropertyName("cost_to_pay")]
        public int CostToPay { get; set; }

        [JsonPropertyName("manual_cost")]
        public int ManualCost { get; set; }

        [JsonPropertyName("cost_per_unit")]
        public int CostPerUnit { get; set; }

        [JsonPropertyName("discount")]
        public int Discount { get; set; }

        [JsonPropertyName("first_cost")]
        public int FirstCost { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }
}
