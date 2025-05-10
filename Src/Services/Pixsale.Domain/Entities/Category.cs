namespace Pixsale.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; } = string.Empty; // Category name
        public string Description { get; set; } = string.Empty; // Category description
        public Guid? ParentId { get; set; } // Reference to the parent category (if any)
        public List<Product> Products { get; set; } // List of products in this category
        // Navigation properties
        public Category? Parent { get; set; } // Reference to the parent category entity
    }
}
