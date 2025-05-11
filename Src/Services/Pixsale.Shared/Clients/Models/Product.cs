namespace Pixsale.Shared.Clients.Models;

public record Product(
    Guid Id,
    string Name,
    string Description,
    Category Category,
    int TotalQty
);

public record Category(Guid Id, string Name);