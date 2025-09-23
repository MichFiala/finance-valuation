using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Shared.Endpoints.Delete;

internal class DeleteHandler(UserManager<User.Models.User> userManager)
{
    public async Task<EmptyResponse> HandleAsync<TEntity>(
        int id,
        ICrudDomainRepository<TEntity> crudDomainRepository,
        HttpContext httpContext) where TEntity : class
    {
        User.Models.User? user = await userManager.FindByEmailAsync(httpContext.Email()) ?? throw new AuthenticationException($"User not found by email {httpContext.Email()}");

        await crudDomainRepository.DeleteAsync(id, user.Id);

        return new EmptyResponse();
    }
}

