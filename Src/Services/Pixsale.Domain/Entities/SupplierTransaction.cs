namespace Pixsale.Domain.Entities
{
    public class SupplierTransaction: BaseEntity
    {

        public Guid SupplierId { get; set; } // Reference to the Supplier
        public DateTime TransactionDate { get; set; } = DateTime.Now; // Date of the transaction
        public string Description { get; set; } = string.Empty; // Description of the transaction
        public decimal Debit { get; set; } = 0; // Amount owed by the Supplier
        public decimal Credit { get; set; } = 0; // Amount paid by the Supplier
        public decimal Balance { get; set; } = 0; // Running balance after the transaction

        // Navigation property
        public Supplier? Supplier { get; set; } // Reference to the Supplier entity
    }
}
