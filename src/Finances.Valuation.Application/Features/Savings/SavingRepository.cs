using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Savings;

internal class SavingRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Models.Saving>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Savings.ToListAsync();
    }

    public async Task<Models.Saving?> GetAsync(int id)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Savings.FindAsync(id);
    }

    public async Task SaveAsync(Models.Saving saving)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (saving.Id <= 0)
        {
            context.Savings.Add(saving);
            await context.SaveChangesAsync();

            return;
        }

        await context.Savings
                     .Where(s => s.Id == saving.Id)
                     .ExecuteUpdateAsync(s =>
                        s.SetProperty(s => s.Name, s => saving.Name)
                         .SetProperty(s => s.Amount, s => saving.Amount)
                         .SetProperty(s => s.TargetAmount, s => saving.TargetAmount)
                         .SetProperty(s => s.ExpectedMonthlyContributionAmount, s => saving.ExpectedMonthlyContributionAmount)
                     );
    }
}