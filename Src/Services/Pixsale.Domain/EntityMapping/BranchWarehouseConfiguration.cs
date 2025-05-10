using Pixsale.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Pixsale.Domain.EntityMapping;
public class BranchWarehouseConfiguration : IEntityTypeConfiguration<BranchWarehouse>
{
    public void Configure(EntityTypeBuilder<BranchWarehouse> builder)
    {
        builder.HasKey(bw => new { bw.BranchId, bw.WarehouseId });

        builder.HasOne(b => b.Branch)
            .WithMany(b => b.BranchWarehouse)
            .HasForeignKey(b => b.BranchId);

        builder.HasOne(w => w.Warehouse)
            .WithMany(w => w.BranchWarehouse)
            .HasForeignKey(bw => bw.WarehouseId);
    }
}
