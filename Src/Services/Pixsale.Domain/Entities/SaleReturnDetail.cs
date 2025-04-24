namespace Pixsale.Domain.Entities
{
    public class SaleReturnDetail: BaseEntity
    {
        public Guid ReturnId { get; set; }  // Reference to the sale return
        public Guid ProductId { get; set; }  // Reference to the product
        public int Quantity { get; set; } = 0; // Quantity returned
        public decimal UnitPrice { get; set; } = 0; // Unit price of the returned item
        public decimal TotalAmount { get; set; } = 0; // Total amount for the returned item

        // Navigation properties
        public Product? Product { get; set; } // Reference to the product entity
    }
}

