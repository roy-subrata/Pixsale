namespace Pixsale.Application.Models;

public record CreateCustomer(string Name, string Email, string Phone, string Address);
public record UpdateCustomer(Guid Id, string Name, string Email, string Phone, string Address);
public record GetCustomer(Guid Id, string Name, string Email, string Phone, string Address);
