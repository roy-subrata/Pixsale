using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pixsale.Domain.Entities;
using Pixsale.Domain.EntityMapping;

namespace Pixsale.Infrastructure
{
    public class PixsaleDbContext : DbContext
    {
        public PixsaleDbContext(DbContextOptions<PixsaleDbContext> options) : base(options) { }

        // DbSets for entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationDetail> QuotationDetails { get; set; }
        public DbSet<WarrantyClaim> WarrantyClaims { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductLocation> ProductLocations { get; set; }
 
        public DbSet<BranchWarehouse> BranchWarehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BranchWarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());

            // Apply configurations for AuditableEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedDate").IsRequired();
                    modelBuilder.Entity(entityType.ClrType).Property<string>("CreatedBy").HasMaxLength(256);
                    modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("ModifiedDate");
                    modelBuilder.Entity(entityType.ClrType).Property<string>("ModifiedBy").HasMaxLength(256);
                    //modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted").HasDefaultValue(false);

                    // Apply global query filter for IsDeleted == false
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, "IsDeleted"),
                            Expression.Constant(false)
                        ),
                        parameter
                    );
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

            // Configure relationships and keys
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Example: Configure PurchaseReturn and PurchaseReturnDetail
            modelBuilder.Entity<PurchaseReturn>()
                .HasMany(pr => pr.ReturnDetails)
                .WithOne()
                .HasForeignKey(prd => prd.ReturnId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseReturnDetail>()
                .HasOne(prd => prd.Product)
                .WithMany()
                .HasForeignKey(prd => prd.ProductId);

            // Example: Configure WarrantyClaim relationships
            modelBuilder.Entity<WarrantyClaim>()
                .HasOne(wc => wc.Product)
                .WithMany(p => p.WarrantyClaims)
                .HasForeignKey(wc => wc.ProductId);

            modelBuilder.Entity<WarrantyClaim>()
                .HasOne(wc => wc.Customer)
                .WithMany()
                .HasForeignKey(wc => wc.CustomerId);

            modelBuilder.Entity<WarrantyClaim>()
                .HasOne(wc => wc.ServiceProvider)
                .WithMany()
                .HasForeignKey(wc => wc.ServiceProviderId);

            // Example: Configure CustomerTransaction relationships
            modelBuilder.Entity<CustomerTransaction>()
                .HasOne(ct => ct.Customer)
                .WithMany(c => c.Transactions)
                .HasForeignKey(ct => ct.CustomerId);

            // Example: Configure SupplierTransaction relationships
            modelBuilder.Entity<SupplierTransaction>()
                .HasOne(st => st.Supplier)
                .WithMany(s => s.Transactions)
                .HasForeignKey(st => st.SupplierId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Automatically set CreatedDate, CreatedBy, ModifiedDate, and ModifiedBy
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("CreatedBy").CurrentValue = "System"; // Replace with actual user
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("ModifiedBy").CurrentValue = "System"; // Replace with actual user
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
