using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Spendings.Models;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Infrastructure.Persistence;

internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Debt> Debts { get; set; }

    public DbSet<Income> Incomes { get; set; }

    public DbSet<Saving> Savings { get; set; }

    public DbSet<Spending> Spendings { get; set; }
    
    public DbSet<Strategy> Strategies { get; set; }

    public DbSet<StrategyConfiguration> StrategiesConfigurations { get; set; }

    public DbSet<Investment> Investments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("fin");

        modelBuilder.Entity<Debt>(mb =>
        {
            mb.ToTable("debts");
            mb.HasKey(d => d.Id);
            mb.HasOne(d => d.Saving)
              .WithMany()
              .HasForeignKey(d => d.SavingId)
              .OnDelete(DeleteBehavior.SetNull);

            mb.Property(p => p.DebtType)
              .HasConversion<string>();
        });

        modelBuilder.Entity<Income>(mb =>
        {
            mb.ToTable("incomes");
            mb.HasKey(i => i.Id);
        });

        modelBuilder.Entity<Saving>(mb =>
        {
            mb.ToTable("savings");
            mb.HasKey(s => s.Id);
        });

        modelBuilder.Entity<Spending>(mb =>
        {
            mb.ToTable("spendings");
            mb.HasKey(s => s.Id);
            mb.Property(p => p.Frequency)
              .HasConversion<string>();
        });

        modelBuilder.Entity<Strategy>(mb =>
        {
            mb.ToTable("strategies");
            mb.HasKey(s => s.Id);
        });

        modelBuilder.Entity<StrategyConfiguration>(mb =>
        {
            mb.ToTable("strategies_configurations");
            mb.HasKey(s => s.Id);
            mb.Property(p => p.Type)
              .HasConversion<string>();
        });

        modelBuilder.Entity<Investment>(mb =>
        {
            mb.ToTable("investments");
            mb.HasKey(i => i.Id);
        });
    }
}
