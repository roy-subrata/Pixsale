namespace Pixsale.Domain.Entities
{
    public class Purchase: AuditableEntity
    {
        public Purchase()
        {
            PurchaseDetails = new List<PurchaseDetail>();
        }

        public Guid SupplierId { get; set; } // Reference to the supplier
        public Guid PurchaseDate { get; set; } // Date the purchase was made
        public decimal SubTotal { get; set; } = 0; // Total before VAT
        public decimal TotalVAT { get; set; } = 0; // Total VAT amount
        public decimal TotalAmount { get; set; } = 0; // Total amount including VAT
        public string Status { get; set; } = "Pending"; // Status (e.g., Pending, Completed, Cancelled)
        public string Notes { get; set; } = string.Empty; // Additional notes or comments
        public List<PurchaseDetail> PurchaseDetails { get; set; } // List of items in the purchase
    }
}
