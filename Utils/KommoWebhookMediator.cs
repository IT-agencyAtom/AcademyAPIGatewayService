using CrmIntegration.Services;
using CrmIntegration.Services.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CrmIntegration.Utils
{
    public static class KommoWebhookMediator
    {
        public const string LeadIdKey = "leads[status][0][id]";
        public const string LeadNewStatusIdKey = "leads[status][0][status_id]";
        public const string CatalogContactKey = "catalogs[add][0][custom_fields][3][values][0][value][entity_id]";
        public const string CatalogUnitPriceKey = "catalogs[add][0][custom_fields][1][values][0][value][unit_price]";
        public const string CatalogProductIdKey = "catalogs[add][0][custom_fields][1][values][0][value][product_id]";
        public const string CatalogQuantityKey = "catalogs[add][0][custom_fields][1][values][0][value][quantity]";
        public const string CatalogDiscountKey = "catalogs[add][0][custom_fields][1][values][0][value][discount][value]";
        public const string CatalogCostKey = "catalogs[add][0][custom_fields][2][values][0][value]";

        public static async Task HandleAsync(IIntegrationService integrationService, IFormCollection form, CancellationToken cancellationToken = default)
        {
            if (form?.Any() != true)
            {
                Debug.WriteLine("No form object found.");
                return;
            }

            if (form.ContainsKey(LeadIdKey) && form.ContainsKey(LeadNewStatusIdKey))
            {
                if (!long.TryParse(form[LeadIdKey], out long leadId))
                {
                    Debug.WriteLine("Lead id must be a long type value.");
                    return;
                }

                if (!long.TryParse(form[LeadNewStatusIdKey], out long statusId))
                {
                    Debug.WriteLine("Lead status id must be a long type value.");
                    return;
                }

                var leadStatusChangedInput = new HandleLeadStatusChangedInput
                {
                    LeadId = leadId,
                    NewStatusId = statusId
                };

                await integrationService.HandleLeadStatusChangedAsync(leadStatusChangedInput, cancellationToken);
                return;
            }

            if (form.TryGetValue(CatalogUnitPriceKey, out var unitPriceValue) &&
                form.TryGetValue(CatalogContactKey, out var contactValue) &&
                form.TryGetValue(CatalogProductIdKey, out var productIdValue))
            {
                if (!decimal.TryParse(unitPriceValue, out decimal unitPrice))
                {
                    Debug.WriteLine("Unit price must be a decimal type value.");
                    return;
                }

                if (!long.TryParse(contactValue, out long contactId))
                {
                    Debug.WriteLine("Contact id must be a long type value.");
                    return;
                }

                if (!long.TryParse(productIdValue, out long productId))
                {
                    Debug.WriteLine("Product id must be a long type value.");
                    return;
                }

                var handleInvoiceCreatedInput = new HandleInvoiceCreatedInput
                {
                    UnitPrice = unitPrice,
                    ContactId = contactId,
                    ProductId = productId
                };

                if (form.TryGetValue(CatalogQuantityKey, out var quantityValue) && long.TryParse(quantityValue, out long quantity))
                {
                    handleInvoiceCreatedInput.Quantity = quantity;
                }

                if (form.TryGetValue(CatalogDiscountKey, out var discountValue) && decimal.TryParse(discountValue, out decimal discount))
                {
                    handleInvoiceCreatedInput.Discount = discount;
                }

                if (form.TryGetValue(CatalogCostKey, out var costValue) && decimal.TryParse(costValue, out decimal cost))
                {
                    handleInvoiceCreatedInput.Cost = cost;
                }

                await integrationService.HandleInvoiceCreatedAsync(handleInvoiceCreatedInput, cancellationToken);
                return;
            }
        }
    }
}
