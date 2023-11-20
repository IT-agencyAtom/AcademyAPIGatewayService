using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class CreateDocumentRoot
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public CreateDocumentOutput Data { get; set; }
    }

    public class CreateDocumentOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
