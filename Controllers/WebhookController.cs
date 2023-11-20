using CrmIntegration.Models;
using CrmIntegration.Services;
using CrmIntegration.Services.Models;
using CrmIntegration.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CrmIntegration.Controllers
{
    [Controller]
    [Route("webhook")]
    public class WebhookController : Controller
    {
        private readonly IIntegrationService _integrationService;

        public WebhookController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        [HttpPost("kommo")]
        public async Task<IActionResult> Kommo()
        {
            if (HttpContext?.Request?.HasFormContentType == true)
            {
                var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(5)).Token;
                await KommoWebhookMediator.HandleAsync(_integrationService, HttpContext?.Request?.Form, cancellationToken);
            }

            return NoContent();
        }

        [HttpPost("altegio")]
        public async Task<IActionResult> Altegio([FromBody] AltegioWebhookRequest request)
        {
            var client = request?.Data?.Client;
            var service = request?.Data?.Services?.FirstOrDefault();
            if (client == null || service == null)
            {
                return BadRequest();
            }

            var input = new HandleBookingCreatedInput
            {
                Phone = client.Phone,
                ClientId = client.Id,
                Name = client.Name,
                Service = service
            };

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(5)).Token;
            await _integrationService.HandleBookingCreatedAsync(input, cancellationToken);
            return NoContent();
        }
    }
}
