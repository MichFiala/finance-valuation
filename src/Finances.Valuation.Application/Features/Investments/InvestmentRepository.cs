using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Shared.Repositories;

namespace Finances.Valuation.Application.Features.Investments;

internal class InvestmentRepository(CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Investment>
{
    public async Task<IReadOnlyCollection<Investment>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Investment>(userId);
    public async Task<Investment?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Investment>(id, userId);
    public async Task<Investment> SaveAsync(Investment investment) => await crudDomainRepository.SaveAsync(investment, s =>
                s.SetProperty(inv => inv.Name, inv => investment.Name)
                 .SetProperty(inv => inv.Amount, inv => investment.Amount));

    public async Task DeleteAsync(int id, string userId) => await crudDomainRepository.DeleteAsync<Investment>(id, userId);
}
