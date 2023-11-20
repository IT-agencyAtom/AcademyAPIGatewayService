using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class CreateDocumentInput
    {
        [JsonIgnore]
        public long CompanyId { get; set; }
        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("storage_id")]
        public long StorageId { get; set; }

        [JsonPropertyName("create_date")]
        public string CreateDate { get; set; }
    }
}
