namespace Pixsale.Shared.Clients.Models;
public record Customer(
    Guid Id,
    string Name,
    string Gender,
    string Email,
    string Phone,
    string Address,
    string City,
    string State,
    string Country,
    string ZipCode
);
