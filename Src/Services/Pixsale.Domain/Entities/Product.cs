namespace Pixsale.Domain.Entities
{
    public class Product: AuditableEntity
    {
        public Product()
        {
            Category = new Category();
        }
        public string Name { get; set; } = string.Empty; // Product name
        public string Description { get; set; } = string.Empty; // Product description
        public Guid CategoryId { get; set; }
        public int TotalQty { get; set; } = 0; // Total stock quantity across all transactions
        // Navigation properties
        public Category? Category { get; set; } // Reference to the category entity
        public List<WarrantyClaim> WarrantyClaims { get; set; } = new List<WarrantyClaim>(); //
        public List<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>(); // List of stock transactions for this product
    }
}
