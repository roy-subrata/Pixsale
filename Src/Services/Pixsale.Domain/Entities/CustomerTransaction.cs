namespace Pixsale.Domain.Entities
{
    public class CustomerTransaction: BaseEntity
    {
        public CustomerTransaction()
        {
            Customer = new Customer();
        }
        public Guid CustomerId { get; set; } 
        public DateTime TransactionDate { get; set; } = DateTime.Now; // Date of the transaction
        public string Description { get; set; } = string.Empty; // Description of the transaction
        public decimal Debit { get; set; } = 0; // Amount owed by the customer
        public decimal Credit { get; set; } = 0; // Amount paid by the customer
        public decimal Balance { get; set; } = 0; // Running balance after the transaction

        // Navigation property
        public Customer Customer { get; set; } // Reference to the customer entity
    }
}
