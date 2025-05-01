namespace Pixsale.Domain.Entities
{
    public class StockTransaction : BaseEntity
    {
        public Guid ProductId { get; set; }  // Reference to the product
        public required Product Product { get; set; } // Reference to the product entity

        public Guid SourceType { get; set; } //Type of source (e.g., "Purchase", "Sale", "Adjustment", "Transfer")
        public Guid SourceId { get; set; } // Reference to the source (e.g., Purch4aseId, SaleId, AdjustmentId)
        public int Quantity { get; set; } = 0; // Quantity added or reduced
        public decimal UnitPrice { get; set; } = 0; // Price per unit
        public decimal TotalAmount { get; set; } = 0; // Total amount (Quantity * UnitPrice)
        public Guid UnitId { get; set; } // Reference to the base unit
        public required Unit Unit { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now; // Date and time of the transaction
        public string Notes { get; set; } = string.Empty; // Additional notes or comments
        public Guid WarehouseId { get; set; } // Reference to the warehouse
        public Warehouse Warehouse { get; set; } = null!; // Reference to the source warehouse
        public Guid? TargetWarehouseId { get; set; } // Reference to the target warehouse (for transfers)
        public Warehouse? TargetWarehouse { get; set; } // Reference to the target warehouse (optional)
                                                        // Navigation properties

    }
}
