using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Investments;

internal class InvestmentRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<Investment?> GetAsync(int id)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        return await context.Investments.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<Investment>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        return await context.Investments.ToListAsync();
    }

    public async Task SaveAsync(Investment investment)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (investment.Id <= 0)
        {
            context.Investments.Add(investment);
            await context.SaveChangesAsync();
            return;
        }

        await context.Investments
            .Where(i => i.Id == investment.Id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(inv => inv.Name, inv => investment.Name)
                 .SetProperty(inv => inv.Amount, inv => investment.Amount)
            );
    }
}
