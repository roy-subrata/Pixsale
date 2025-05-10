namespace Pixsale.Application.Models;
public record CreateBranch(string Name, string Description, string Address);
public record UpdateBranch(Guid Id, string Name, string Description, string Address);
public record GetBranch(Guid Id, string Name, string Description, string Address);
