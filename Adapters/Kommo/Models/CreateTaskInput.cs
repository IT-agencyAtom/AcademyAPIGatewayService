using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class CreateTaskInput
    {
        [JsonPropertyName("task_type_id")]
        public int TaskTypeId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("complete_till")]
        public int CompleteTill { get; set; }

        [JsonPropertyName("entity_id")]
        public long EntityId { get; set; }

        [JsonPropertyName("entity_type")]
        public string EntityType { get; set; }
    }
}
