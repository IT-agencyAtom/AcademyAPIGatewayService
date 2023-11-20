using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class LeadContact
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }
    }

    public class LeadOutput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("responsible_user_id")]
        public int ResponsibleUserId { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonPropertyName("pipeline_id")]
        public int PipelineId { get; set; }

        [JsonPropertyName("loss_reason_id")]
        public object LossReasonId { get; set; }

        [JsonPropertyName("created_by")]
        public int CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public int UpdatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("closed_at")]
        public object ClosedAt { get; set; }

        [JsonPropertyName("closest_task_at")]
        public object ClosestTaskAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public object CustomFieldsValues { get; set; }

        [JsonPropertyName("score")]
        public object Score { get; set; }

        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }

        [JsonPropertyName("labor_cost")]
        public object LaborCost { get; set; }

        [JsonPropertyName("is_price_computed")]
        public bool IsPriceComputed { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("_embedded")]
        public LeadEmbedded Embedded { get; set; }
    }
}
