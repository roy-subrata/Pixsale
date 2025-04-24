namespace Pixsale.Domain.Entities
{
    public class SaleReturn : AuditableEntity
    {
        public Guid CustomerId { get; set; }  // Reference to the customer
        public DateTime ReturnDate { get; set; } = DateTime.Now; // Date of the return
        public decimal TotalAmount { get; set; } = 0; // Total amount of the return
        public string Status { get; set; } = "Draft"; // Status of the return (e.g., Draft, Confirmed, Completed)
        public string Notes { get; set; } = string.Empty; // Additional notes or comments

        // Navigation properties
        public Customer? Customer { get; set; } // Reference to the customer entity
        public List<SaleReturnDetail> ReturnDetails { get; set; } = new List<SaleReturnDetail>(); // List of returned items
    }
}

