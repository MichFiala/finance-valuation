using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.SavingsLongevity.Get.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.SavingsLongevity.Get;

internal class GetSavingsLongevityEndpoint(
    UserManager<User.Models.User> userManager,
    SavingsLongevityCalculationService longevityService)
    : Endpoint<EmptyRequest, GetSavingsLongevityResponse>
{
    public override void Configure()
    {
        Get("/savings-longevity");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Returns how many months your savings and investments will last based on spendings.";
            s.Description = "Calculates the duration your savings and investments can cover your spendings.";
        });
    }

    public override async Task HandleAsync(EmptyRequest emptyRequest, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");        

        int longevity = await longevityService.CalculateMonthsOfLongevityAsync(user.Id);

        var grade = SavingsLongevityCalculationService.ValuateGrade(longevity);

        await Send.OkAsync(new GetSavingsLongevityResponse
        {
            Months = longevity,
            Till = DateOnly.FromDateTime(DateTime.Now.AddMonths(longevity)),
            Grade = grade
        }, ct);
    }
}