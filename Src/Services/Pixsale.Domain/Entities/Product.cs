namespace Pixsale.Domain.Entities
{
    public class Product : AuditableEntity
    {
        
        public string Name { get; set; } = string.Empty;
        public string LocalName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public Guid CategoryId { get; set; }
        public int TotalQty { get; set; } = 0;
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public List<WarrantyClaim> WarrantyClaims { get; set; } = new List<WarrantyClaim>(); //
        public List<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>(); // List of stock transactions for this product
    }
}
