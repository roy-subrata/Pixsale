namespace Pixsale.Shared.Clients.Models;

public record Branch(
    Guid Id,
    string Name,
    string Description,
    string Address,
    DateTime CreatedAt,
    DateTime UpdatedAt
);