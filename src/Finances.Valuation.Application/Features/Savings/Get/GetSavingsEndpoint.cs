using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Get.Models;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Savings.Get;

internal class GetSavingsEndpoint(UserManager<User.Models.User> userManager, SavingRepository savingRepository) : Endpoint<EmptyRequest, GetSavingsResponse>
{
    public override void Configure()
    {
        Get("/savings");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets all savings";
            s.Description = "Returns a list of all savings DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        IReadOnlyCollection<Saving>? savings = await savingRepository.GetAsync(user.Id);

        var savingDtos = savings.Select(SavingDto.Create).ToList();

        await Send.OkAsync(new GetSavingsResponse
        {
            Savings = savingDtos
        }, ct);
    }
}

