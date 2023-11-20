using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CatalogInvoiceItemDiscount
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }
    }

    public class CatalogInvoiceItem
    {
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("unit_price")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }
    }
}
