namespace Pixsale.Application.Models;

public record CreateWarehouse(string Name, string Description, string Address);
public record UpdateWarehouse(Guid Id, string Name, string Description, string Address);
public record GetWarehouse(Guid Id, string Name, string Description, string Address);
