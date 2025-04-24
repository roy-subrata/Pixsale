namespace Pixsale.Domain.Entities
{
    public class Quotation: AuditableEntity
    {
        public Quotation()
        {
            QuotationDetails = new List<QuotationDetail>();
        }
        public Guid CustomerId { get; set; } // Reference to the customer
        public string QuotationDate { get; set; } = string.Empty; // Date the quotation was created
        public decimal SubTotal { get; set; } = 0; // Total before VAT
        public decimal TotalVAT { get; set; } = 0; // Total VAT amount
        public decimal TotalAmount { get; set; } = 0; // Total amount including VAT
        public string Status { get; set; } = "Pending"; // Status (e.g., Pending, Confirmed, Rejected)
        public List<QuotationDetail> QuotationDetails { get; set; } // List of items in the quotation
    }
}
