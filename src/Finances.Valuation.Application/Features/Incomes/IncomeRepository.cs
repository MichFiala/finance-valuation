using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Shared.Repositories;

namespace Finances.Valuation.Application.Features.Incomes;

internal class IncomeRepository(CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Income>
{
    public async Task<IReadOnlyCollection<Income>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Income>(userId);
    public async Task<Income?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Income>(id, userId);
    public async Task<Income> SaveAsync(Income income) => await crudDomainRepository.SaveAsync(income, s =>
                        s.SetProperty(i => i.Name, i => income.Name)
                         .SetProperty(i => i.Amount, i => income.Amount)
                         .SetProperty(i => i.Date, i => income.Date)
                         .SetProperty(i => i.IsMainIncome, i => income.IsMainIncome));

    public async Task DeleteAsync(int id, string userId) => await crudDomainRepository.DeleteAsync<Income>(id, userId);
}
