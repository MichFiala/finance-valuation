using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Spendings;

internal class SpendingRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Models.Spending>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Spendings.ToListAsync();
    }

    public async Task SaveAsync(Models.Spending spending)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (spending.Id <= 0)
        {
            context.Spendings.Add(spending);
            await context.SaveChangesAsync();

            return;
        }

        await context.Spendings
                     .Where(s => s.Id == spending.Id)
                     .ExecuteUpdateAsync(s =>
                        s.SetProperty(s => s.Name, s => spending.Name)
                         .SetProperty(s => s.Amount, s => spending.Amount)
                         .SetProperty(s => s.Frequency, s => spending.Frequency)
                     );
    }
}