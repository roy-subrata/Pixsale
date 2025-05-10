namespace Pixsale.Application.Models;

public record CreateCustomer(
    string Name,
    string NationalId,
    string Email,
    string Phone,
    string Address,
    string City,
    string State,
    string Country,
    string ZipCode,
    string Gender
    );
public record UpdateCustomer(
    Guid Id,
    string Name,
    string NationalId,
    string Email,
    string Phone,
    string Address,
    string City,
    string State,
    string Country,
    string ZipCode,
    string Gender);
public record GetCustomer(
    Guid Id,
    string Name,
    string NationalId,
    string Email,
    string Phone,
    string Address,
    string City,
    string State,
    string Country,
    string ZipCode,
    string Gender
    );
