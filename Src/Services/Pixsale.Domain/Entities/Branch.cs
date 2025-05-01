namespace Pixsale.Domain.Entities;
public class Branch : AuditableEntity
{
    public string Name { get; set; } = string.Empty; // Branch name
    public string Address { get; set; } = string.Empty; // Branch address
    public string Description { get; set; } = string.Empty; // Additional details about the branch

    // Navigation properties
    public ICollection<BranchWarehouse> BranchWarehouse { get; set; } = new List<BranchWarehouse>(); // List of warehouses associated with this branch
}

public class Warehouse : AuditableEntity
{
    public string Name { get; set; } = string.Empty; // Warehouse name
    public string Address { get; set; } = string.Empty; // Warehouse address
    public string Description { get; set; } = string.Empty; // Additional details about the warehouse
    public ICollection<BranchWarehouse> BranchWarehouse { get; set; } = new List<BranchWarehouse>();
    public ICollection<ProductLocation> ProductLocations { get; set; } = new List<ProductLocation>(); // List of product locations in this warehouse
}

public class ProductLocation : BaseEntity
{
    public Guid ProductId { get; set; } // Reference to the product
    public Product Product { get; set; } = null!; // Reference to the product entity
    public Guid WarehouseId { get; set; } // Reference to the warehouse
    public Warehouse Warehouse { get; set; } = null!; // Reference to the warehouse entity
    public string SpecificLocation { get; set; } = string.Empty; // Specific location (e.g., "Shelf A1", "Showcase 3")
    public int Quantity { get; set; } = 0; // Quantity of the product at this location

}


public class BranchWarehouse
{
    public Guid BranchId { get; set; }
    public required Branch Branch { get; set; }
    public Guid WarehouseId { get; set; }
    public required Warehouse Warehouse { get; set; }
}