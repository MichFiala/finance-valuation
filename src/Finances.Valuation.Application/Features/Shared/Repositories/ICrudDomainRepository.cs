namespace Finances.Valuation.Application.Features.Shared.Repositories;

internal interface ICrudDomainRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetAsync(int id, string userId);

    Task<TEntity> SaveAsync(TEntity entity);

    Task DeleteAsync(int id, string userId);
}
