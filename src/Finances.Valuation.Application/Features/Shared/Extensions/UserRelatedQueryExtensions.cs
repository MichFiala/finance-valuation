using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Shared.Extensions;

internal static class UserRelatedQueryExtensions
{
    public static IQueryable<TEntity> OfUser<TEntity>(this IQueryable<TEntity> query, string userId) where TEntity : IUserRelated
        => query.Where(entity => entity.UserId == userId);
}
