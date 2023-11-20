using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class CreateGoodsTransactionInput
    {
        [JsonIgnore]
        public long CompanyId { get; set; }

        [JsonPropertyName("document_id")]
        public long DocumentId { get; set; }

        [JsonPropertyName("good_id")]
        public long GoodId { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("cost_per_unit")]
        public decimal CostPerUnit { get; set; }

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        [JsonPropertyName("operation_unit_type")]
        public int OperationUnitType { get; set; }

        [JsonPropertyName("master_id")]
        public int MasterId { get; set; }

        [JsonPropertyName("client_id")]
        public long ClientId { get; set; }

        [JsonPropertyName("supplier_id")]
        public int SupplierId { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}
