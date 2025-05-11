namespace Pixsale.Shared.Clients.Models;
public record Unit(
    Guid Id,
    string Name,
    string ShortName,
    decimal ConversionFactor
);
