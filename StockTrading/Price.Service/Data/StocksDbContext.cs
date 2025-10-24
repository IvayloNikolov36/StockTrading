using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prices.Service.Data.Seed;
using Prices.Service.Entities;
using Prices.Service.Entities.Base;

namespace Prices.Service.Data
{
    public class StocksDbContext : DbContext
    {
        public StocksDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StockEntity> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("app");

            modelBuilder.Entity<StockEntity>()
                .HasIndex(s => s.Ticker)
                .IsUnique();

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
            => this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAudit
                    && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                IAudit entity = (IAudit)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            StocksSeeder.Seed(modelBuilder);
        }
    }
}
