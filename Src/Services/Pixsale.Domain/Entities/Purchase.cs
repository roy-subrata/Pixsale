namespace Pixsale.Domain.Entities
{
    public class Purchase: AuditableEntity
    {
        
        public Guid SupplierId { get; set; } // Reference to the supplier
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow; // Date the purchase was made
        public decimal SubTotal { get; set; } = 0; // Total before VAT
        public decimal TotalVAT { get; set; } = 0; // Total VAT amount
        public decimal TotalAmount { get; set; } = 0; // Total amount including VAT
        public PurchaseStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty; // Additional notes or comments
        public List<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>(); // List of items in the purchase
    }

    public enum PurchaseStatus
    {
        Quotation,
        Draft,
        Confirmed,
        Received,
        Cancelled
    }

}
