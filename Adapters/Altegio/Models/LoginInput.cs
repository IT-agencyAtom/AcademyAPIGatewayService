using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class LoginInput
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
