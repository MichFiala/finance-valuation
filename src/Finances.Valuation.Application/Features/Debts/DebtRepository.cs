using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Debts;

internal class DebtRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Models.Debt>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Debts.ToListAsync();
    }

    public async Task SaveAsync(Models.Debt debt)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (debt.Id <= 0)
        {
            context.Debts.Add(debt);
            await context.SaveChangesAsync();

            return;
        }

        await context.Debts
                     .Where(d => d.Id == debt.Id)
                     .ExecuteUpdateAsync(s =>
                        s.SetProperty(d => d.Name, d => debt.Name)
                         .SetProperty(d => d.DebtType, d => debt.DebtType)
                         .SetProperty(d => d.Amount, d => debt.Amount)
                         .SetProperty(d => d.Interest, d => debt.Interest)
                         .SetProperty(d => d.Payment, d => debt.Payment)
                         .SetProperty(d => d.SavingId, d => debt.SavingId)
                     );
    }
}
