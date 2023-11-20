using CrmIntegration.Services.Models;

namespace CrmIntegration.Services
{
    public interface IIntegrationService
    {
        Task HandleLeadStatusChangedAsync(HandleLeadStatusChangedInput input, CancellationToken cancellationToken = default);
        Task HandleBookingCreatedAsync(HandleBookingCreatedInput input, CancellationToken cancellationToken = default);
        Task HandleInvoiceCreatedAsync(HandleInvoiceCreatedInput input, CancellationToken cancellationToken = default);
    }
}
