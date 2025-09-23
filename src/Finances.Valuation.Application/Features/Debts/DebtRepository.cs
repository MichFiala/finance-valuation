using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Shared.Repositories;

namespace Finances.Valuation.Application.Features.Debts;

internal class DebtRepository(CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Debt>
{
    public async Task<IReadOnlyCollection<Debt>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Debt>(userId);
    public async Task<Debt?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Debt>(id, userId);
    public async Task<Debt> SaveAsync(Debt debt)  => await crudDomainRepository.SaveAsync(debt, s =>
                        s.SetProperty(d => d.Name, d => debt.Name)
                         .SetProperty(d => d.DebtType, d => debt.DebtType)
                         .SetProperty(d => d.Amount, d => debt.Amount)
                         .SetProperty(d => d.Interest, d => debt.Interest)
                         .SetProperty(d => d.Payment, d => debt.Payment)
                         .SetProperty(d => d.SavingId, d => debt.SavingId));

    public async Task DeleteAsync(int id, string userId) => await crudDomainRepository.DeleteAsync<Debt>(id, userId);
}
