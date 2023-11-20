namespace CrmIntegration.Options
{
    public class AltegioIntegrationOptions
    {
        public const string SectionName = "Integrations:Altegio";

        public string PartnerToken { get; set; }
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long CompanyId { get; set; }
        public string KommoCustomFieldCode { get; set; }
        public long GoodsStorageId { get; set; }
    }
}
