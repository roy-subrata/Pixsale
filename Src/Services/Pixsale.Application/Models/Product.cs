namespace Pixsale.Application.Models;

public record CreateProduct(
    string Name,
    string LocalName,
    string Brand,
    string Description,
    Guid CategoryId,
    Guid UnitId
    );
public record UpdateProduct(
    Guid Id,
    string Name,
    string LocalName,
    string Brand,
    string Description,
    Guid CategoryId,
    Guid UnitId
    );
public record GetProduct(
    Guid Id,
    string Name,
    string LocalName,
    string Brand,
    string Description,
    EntityRef Category,
    EntityRef Unit,
    int TotalQty);
public record EntityRef(Guid Id, string CatrgoryName);