namespace Pixsale.Domain.Entities
{
    public class PurchaseReturn : AuditableEntity
    {
        public Guid PurchaseId { get; set; }  // Reference to the original purchase
        public Guid SupplierId { get; set; } // Reference to the supplier
        public DateTime ReturnDate { get; set; } = DateTime.Now; // Date of the return
        public decimal TotalAmount { get; set; } = 0; // Total amount of the return
        public string Status { get; set; } = "Draft"; // Status of the return (e.g., Draft, Confirmed, Completed)
        public string Notes { get; set; } = string.Empty; // Additional notes or comments

        // Navigation properties
        public Supplier? Supplier { get; set; } // Reference to the supplier entity
        public List<PurchaseReturnDetail> ReturnDetails { get; set; } = new List<PurchaseReturnDetail>(); // List of returned items
    }
}

