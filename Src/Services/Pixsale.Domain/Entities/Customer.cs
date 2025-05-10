namespace Pixsale.Domain.Entities
{
    public class Customer: AuditableEntity
    {
        public string Name { get; set; } = string.Empty; // Customer name
        public string Gender { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // Customer email
        public string Phone { get; set; } = string.Empty; // Customer phone number
        public string Address { get; set; } = string.Empty; // Customer address
        public string City { get; set; } = string.Empty; // City
        public string State { get; set; } = string.Empty; // State
        public string Country { get; set; } = string.Empty; // Country
        public string ZipCode { get; set; } = string.Empty; // Zip code
        public decimal AccountBalance { get; set; } = 0; // Positive for credit, negative for dues

        // Navigation property
        public int Points { get; set; } = 0; // Loyalty points earned by the customer

        public List<CustomerTransaction> Transactions { get; set; } = new List<CustomerTransaction>(); // Transaction history
    }
}
