using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Repositories;
using Finances.Valuation.Application.Features.Spendings.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Spendings;

internal class SpendingRepository(IDbContextFactory<AppDbContext> dbContextFactory, CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Spending>
{
    public async Task<IReadOnlyCollection<Spending>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Spending>(userId);
    public async Task<IReadOnlyCollection<Spending>> GetMandatoryAsync(string userId)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Spendings.OfUser(userId).Where(s => s.IsMandatory).ToListAsync();
    }
    public async Task<Spending?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Spending>(id, userId);
    public async Task<Spending> SaveAsync(Spending spending) => await crudDomainRepository.SaveAsync(spending, s =>
                        s.SetProperty(s => s.Name, s => spending.Name)
                         .SetProperty(s => s.Amount, s => spending.Amount)
                         .SetProperty(s => s.Frequency, s => spending.Frequency)
                         .SetProperty(s => s.IsMandatory, s => spending.IsMandatory));

    public async Task DeleteAsync(int id, string userId) => await crudDomainRepository.DeleteAsync<Spending>(id, userId);
}