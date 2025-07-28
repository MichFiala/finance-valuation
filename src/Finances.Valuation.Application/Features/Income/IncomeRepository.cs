using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Income;
internal class IncomeRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Models.Income>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Incomes.ToListAsync();
    }

    public async Task SaveAsync(Models.Income income)
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
                         .SetProperty(i => i.Month, i => income.Month)
                     );
    }
}
