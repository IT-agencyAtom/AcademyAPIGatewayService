using CrmIntegration.Models;

namespace CrmIntegration.Services.Models
{
    public class HandleBookingCreatedInput
    {
        public long ClientId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public Service Service { get; set; }
    }
}
