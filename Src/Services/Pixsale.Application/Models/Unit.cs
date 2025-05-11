namespace Pixsale.Application.Models;
public record CreateUnit(string Name, string ShortName, decimal ConversionFactor);
public record UpdateUnit(Guid Id, string Name, string ShortName, decimal ConversionFactor);
public record GetUnit(Guid Id, string Name, string ShortName, decimal ConversionFactor);
