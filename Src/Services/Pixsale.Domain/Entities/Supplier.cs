namespace Pixsale.Domain.Entities
{
    public class Supplier: AuditableEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Points { get; set; } = 0; // Loyalty points earned by the Buyer

        // Navigation properties
        public List<SupplierTransaction> Transactions { get; set; } = new List<SupplierTransaction>(); // Transaction history
    }
}
