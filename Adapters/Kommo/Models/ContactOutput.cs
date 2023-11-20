using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo.Models
{
    public class ContactOutput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("responsible_user_id")]
        public int ResponsibleUserId { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        [JsonPropertyName("created_by")]
        public int CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public int UpdatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("closest_task_at")]
        public object ClosestTaskAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("is_unsorted")]
        public bool IsUnsorted { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public List<CustomFieldsValue> CustomFieldsValues { get; set; }

        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("_embedded")]
        public ContactEmbedded Embedded { get; set; }
    }

    public class ContactEmbedded
    {
        [JsonPropertyName("contacts")]
        public List<ContactOutput> Contacts { get; set; }

        [JsonPropertyName("tags")]
        public List<object> Tags { get; set; }

        [JsonPropertyName("companies")]
        public List<Company> Companies { get; set; }

        [JsonPropertyName("leads")]
        public List<Lead> Leads { get; set; }
    }

    public class ContactRoot
    {
        [JsonPropertyName("_page")]
        public int Page { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("_embedded")]
        public ContactEmbedded Embedded { get; set; }
    }
}
