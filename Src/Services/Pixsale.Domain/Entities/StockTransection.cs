namespace Pixsale.Domain.Entities
{
    public class StockTransaction: BaseEntity
    {
        public StockTransaction()
        {
            Product = new Product();
        }
        public Guid ProductId { get; set; }  // Reference to the product
        public Guid SourceType { get; set; } // Type of source (e.g., "Purchase", "Sale", "Adjustment")
        public Guid SourceId { get; set; }  // Reference to the source (e.g., PurchaseId, SaleId, AdjustmentId)
        public int Quantity { get; set; } = 0; // Quantity added or reduced
        public decimal UnitPrice { get; set; } = 0; // Price per unit
        public decimal TotalAmount { get; set; } = 0; // Total amount (Quantity * UnitPrice)
        public DateTime TransactionDate { get; set; } = DateTime.Now; // Date and time of the transaction
        public string Notes { get; set; } = string.Empty; // Additional notes or comments

        public string StockLocation { get; set; }

        // Navigation properties
        public Product Product { get; set; } // Reference to the product entity
    }
}
