namespace Pixsale.Domain.Entities
{
    public abstract class AuditableEntity: BaseEntity
    {
        public string CreatedBy { get; set; } = string.Empty; // User who created the record
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Timestamp when the record was created
        public string ModifiedBy { get; set; } = string.Empty; // User who last modified the record
        public DateTime? ModifiedDate { get; set; } // Timestamp when the record was last modified
        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
