using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Spendings.Get.Models;
using Finances.Valuation.Application.Features.Spendings.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Spendings.Get;

internal class GetSpendingsEndpoint(UserManager<User.Models.User> userManager, SpendingRepository spendingRepository) : Endpoint<EmptyRequest, GetSpendingsResponse>
{
    public override void Configure()
    {
        Get("/spendings");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets spendings";
            s.Description = "Returns the spending DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");
        
        var spendings = await spendingRepository.GetAsync(user.Id);

        if(spendings is null)
            ThrowError("Spendings not found.");

        await Send.OkAsync(new GetSpendingsResponse
        {
            Spendings = spendings.Select(Spending.Create).ToList()
        }, ct);
    }
}