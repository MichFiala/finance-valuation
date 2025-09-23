using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Get.Models;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Incomes.Get;

internal class GetIncomesEndpoint(UserManager<User.Models.User> userManager, IncomeRepository incomeRepository) 
    : Endpoint<EmptyRequest, GetIncomesResponse>
{
    public override void Configure()
    {
        Get("/incomes");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets incomes";
            s.Description = "Returns the income DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        var incomes = await incomeRepository.GetAsync(user.Id);

        if(incomes is null)
            ThrowError("Incomes not found.");

        await Send.OkAsync(new GetIncomesResponse
        {
            Incomes = incomes.Select(IncomeDto.Create).ToList()
        }, ct);
    }
}
