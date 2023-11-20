namespace CrmIntegration.Options
{
    public class KommoIntegrationOptions
    {
        public const string SectionName = "Integrations:Kommo";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SubUri { get; set; }
        public string MainUri { get; set; }
        public string RedirectUri { get; set; }
        public string AuthorizationCode { get; set; }
        public Dictionary<string, long> FieldIds { get; set; }
        public string TaskTitle { get; set; }
        public TimeSpan TaskCompleteTill { get; set; }
    }
}
