namespace Pixsale.Application.Models;

public record CreateCategory(Guid Id, string Name, string Description, Guid? ParentId);
public record GetCategory(Guid Id, string Name, string Description);
public record UpdateCategory(Guid Id, string Name, string Description, Guid? ParentId);

