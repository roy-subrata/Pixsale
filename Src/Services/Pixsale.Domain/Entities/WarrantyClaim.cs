namespace Pixsale.Domain.Entities
{
    public class WarrantyClaim : AuditableEntity
    {
        public Guid ProductId { get; set; } // Reference to the product
        public Guid CustomerId { get; set; }
        public Guid ServiceProviderId { get; set; } // Reference to the service provider (supplier or your business)
        public DateTime ClaimDate { get; set; } = DateTime.Now; // Date the claim was made
        public DateTime ExpirationDate { get; set; } // Expiration date of the warranty
        public string Status { get; set; } = "Pending"; // Status of the claim (e.g., Pending, Approved, Rejected, Completed)
        public string Notes { get; set; } = string.Empty; // Additional notes or comments

        // Navigation properties
        public Product? Product { get; set; } // Reference to the product entity
        public Customer? Customer { get; set; } // Reference to the customer entity
        public Supplier? ServiceProvider { get; set; } // Reference to the service provider (supplier or your business)
    }
}