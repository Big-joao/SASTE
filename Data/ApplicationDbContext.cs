using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SASTE.Models;

namespace SASTE.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }

    // DbSets
    public DbSet<Product> Products => Set<Product>();
    public DbSet<RoleChangeAudit> RoleChangeAudits => Set<RoleChangeAudit>();

    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<MonthlyGoalGlobal> MonthlyGoalGlobals => Set<MonthlyGoalGlobal>();
    public DbSet<MonthlyGoalUser> MonthlyGoalUsers => Set<MonthlyGoalUser>();

    // 👇 AQUI
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configurações dos modelos (índices, relações, etc.)
        builder.Entity<Product>()
            .HasIndex(p => p.Sku)
            .IsUnique();
        builder.Entity<RoleChangeAudit>()
            .HasIndex(x => new { x.TargetUserId, x.ChangedAtUtc });

        builder.Entity<StockMovement>()
            .HasOne(sm => sm.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(sm => sm.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Sale>()
            .HasIndex(s => s.SaleNumber)
            .IsUnique();

        builder.Entity<SaleItem>()
            .HasOne(i => i.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SaleItem>()
            .HasOne(i => i.Product)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<MonthlyGoalGlobal>()
            .HasIndex(g => new { g.Year, g.Month })
            .IsUnique();

        builder.Entity<MonthlyGoalUser>()
            .HasIndex(g => new { g.Year, g.Month, g.UserId })
            .IsUnique();
    }
}
