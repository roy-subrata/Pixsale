namespace Pixsale.Domain.Entities;
public class Unit : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string shortName { get; set; } = string.Empty;
    public decimal ConversionFactor { get; set; } = 0;
}