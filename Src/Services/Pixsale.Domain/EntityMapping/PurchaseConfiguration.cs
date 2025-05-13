using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixsale.Domain.Entities;

namespace Pixsale.Domain.EntityMapping
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}
