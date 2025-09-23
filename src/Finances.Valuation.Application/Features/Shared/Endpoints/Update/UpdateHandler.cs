using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Models;
using Finances.Valuation.Application.Features.Shared.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Shared.Endpoints.Update;

internal class UpdateHandler(UserManager<User.Models.User> userManager)
{
    public async Task<EmptyResponse> HandleAsync<TEntityDto, TEntity>(
        TEntityDto request,
        Action<TEntity> updateExpression,
        ICrudDomainRepository<TEntity> crudDomainRepository,
        HttpContext httpContext) where TEntity : class where TEntityDto: IEntityDto
    {
        User.Models.User? user = await userManager.FindByEmailAsync(httpContext.Email()) ?? throw new System.Security.Authentication.AuthenticationException($"User not found by email {httpContext.Email()}");

        TEntity entity = await crudDomainRepository.GetAsync(request.Id, user.Id) ?? throw new ArgumentException($"{nameof(TEntity)} with id {request.Id} not found", nameof(request));

        updateExpression(entity);

        await crudDomainRepository.SaveAsync(entity);

        return new EmptyResponse();
    }
}

