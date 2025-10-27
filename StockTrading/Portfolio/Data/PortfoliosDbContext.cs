using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portfolio.Service.Data.Seed;
using Portfolio.Service.Entities;
using Portfolio.Service.Entities.Base;

namespace Portfolio.Service.Data;

public class PortfoliosDbContext : DbContext
{
    public PortfoliosDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<StockEntity> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");

        modelBuilder.Entity<StockEntity>()
            .HasIndex(s => s.Ticker)
            .IsUnique();

        StocksSeeder.Seed(modelBuilder);

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
}
