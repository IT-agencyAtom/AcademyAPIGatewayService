using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class UpdateClientInput : CreateClientInput
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
