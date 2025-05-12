namespace Pixsale.Application.Models;

public record CreateSupplier(
        Guid Id,
        string Name,
        string Email,
        string Phone,
        string Address,
        string City,
        string State,
        string Country

    );

public record GetSupplier(
    Guid Id,
    string Name,
    string Email,
    string NationalId,
    string Phone,
    string Address,
    string City,
    string State,
    string Country
    );


public record UpdateSupplier(
    Guid Id,
    string Name,
    string Email,
    string NationalId,
    string Phone,
    string Address,
    string City,
    string State,
    string Country
);