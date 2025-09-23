using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Repositories;

namespace Finances.Valuation.Application.Features.Savings;

internal class SavingRepository(CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Saving>
{
    public async Task<IReadOnlyCollection<Saving>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Saving>(userId);
    public async Task<Saving?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Saving>(id, userId);
    public async Task<Saving> SaveAsync(Saving saving) => await crudDomainRepository.SaveAsync(saving, s =>
                s.SetProperty(s => s.Name, s => saving.Name)
                         .SetProperty(s => s.Amount, s => saving.Amount)
                         .SetProperty(s => s.TargetAmount, s => saving.TargetAmount)
                         .SetProperty(s => s.ExpectedMonthlyContributionAmount, s => saving.ExpectedMonthlyContributionAmount));

    public async Task DeleteAsync(int id, string userId) => await crudDomainRepository.DeleteAsync<Saving>(id, userId);
}