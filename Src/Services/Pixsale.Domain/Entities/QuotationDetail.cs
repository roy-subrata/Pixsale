namespace Pixsale.Domain.Entities
{

    public class QuotationDetail: BaseEntity
    {
        public Guid QuotationId { get; set; }  // Reference to the parent quotation
        public Guid ProductId { get; set; } // Reference to the product
        public int Quantity { get; set; } = 0; // Quoted quantity
        public decimal UnitPrice { get; set; } = 0; // Unit price of the product
        public decimal VATRate { get; set; } = 0; // VAT rate (e.g., 0.05 for 5%)
        public decimal VATAmount { get; set; } = 0; // VAT amount for this line item
        public decimal TotalAmount { get; set; } = 0; // Total amount (UnitPrice * Quantity + VATAmount)
    }
}
