namespace CrmIntegration.Services.Models
{
    public class HandleInvoiceCreatedInput
    {
        public decimal UnitPrice { get; set; }
        public long ContactId { get; set; }
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Cost { get; set; }
    }
}
