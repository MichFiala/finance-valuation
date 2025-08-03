using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Incomes;
internal class IncomeRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Income>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Incomes.ToListAsync();
    }

    public async Task SaveAsync(Income income)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (income.Id <= 0)
        {
            context.Incomes.Add(income);
            await context.SaveChangesAsync();

            return;
        }

        await context.Incomes
                     .Where(d => d.Id == income.Id)
                     .ExecuteUpdateAsync(s =>
                        s.SetProperty(i => i.Name, i => income.Name)
                         .SetProperty(i => i.Amount, i => income.Amount)
                         .SetProperty(i => i.Date, i => income.Date)
                     );
    }
}
