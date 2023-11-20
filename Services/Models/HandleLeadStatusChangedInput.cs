namespace CrmIntegration.Services.Models
{
    public class HandleLeadStatusChangedInput
    {
        public long LeadId { get; set; }
        public long NewStatusId { get; set; }
    }
}
