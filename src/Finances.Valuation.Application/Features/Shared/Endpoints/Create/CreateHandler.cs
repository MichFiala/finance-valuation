using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Repositories;

namespace Finances.Valuation.Application.Features.Shared.Endpoints.Create;

internal class CreateHandler(UserManager<User.Models.User> userManager)
{
    public async Task<EmptyResponse> HandleAsync<TEntityDto, TEntity>(
        TEntityDto request,
        Func<TEntityDto, string, TEntity> createExpression,
        ICrudDomainRepository<TEntity> crudDomainRepository,
        HttpContext httpContext) where TEntity: class
    {
        User.Models.User? user = await userManager.FindByEmailAsync(httpContext.Email()) ?? throw new System.Security.Authentication.AuthenticationException($"User not found by email {httpContext.Email()}");

        TEntity entity = createExpression(request, user.Id);

        await crudDomainRepository.SaveAsync(entity);

        return new EmptyResponse();
    }
}


