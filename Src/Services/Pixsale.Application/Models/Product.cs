namespace Pixsale.Application.Models;

public record CreateProduct(
    string Name,
    string Description,
    Guid CategoryId
    );
public record UpdateProduct(
    Guid Id,
    string Name,
    string Description,
    Guid CategoryId
    );
public record GetProduct(
    Guid Id,
    string Name,
    string Description,
    Category Category,
    int TotalQty);
public record Category(Guid Id, string CatrgoryName);