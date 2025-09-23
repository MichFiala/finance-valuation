using System.Linq.Expressions;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Finances.Valuation.Application.Features.Shared.Repositories;

internal class CrudDomainRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<TEntity>> GetAsync<TEntity>(string userId) where TEntity : class, IDatabaseEntry, IUserRelated
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Set<TEntity>().OfUser(userId).ToListAsync();
    }
    public async Task<TEntity?> GetAsync<TEntity>(int id, string userId) where TEntity : class, IDatabaseEntry, IUserRelated
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        TEntity? entity = await context.Set<TEntity>().FindAsync(id);

        if (entity is not null && entity.UserId == userId)
            return entity;

        return null;
    }
    public async Task<TEntity> SaveAsync<TEntity>(TEntity entity, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyExpression) where TEntity : class, IDatabaseEntry, IUserRelated
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (entity.Id <= 0)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        await context.Set<TEntity>()
                     .OfUser(entity.UserId)
                     .Where(d => d.Id == entity.Id)
                     .ExecuteUpdateAsync(setPropertyExpression);

        return entity;
    }

    public async Task DeleteAsync<TEntity>(int id, string userId) where TEntity : class, IDatabaseEntry, IUserRelated
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        await DeleteAsync<TEntity>(id, userId, context);
    }
    
    public static async Task DeleteAsync<TEntity>(int id, string userId, AppDbContext context) where TEntity : class, IDatabaseEntry, IUserRelated => await context.Set<TEntity>().OfUser(userId).Where(d => d.Id == id).ExecuteDeleteAsync();
}
