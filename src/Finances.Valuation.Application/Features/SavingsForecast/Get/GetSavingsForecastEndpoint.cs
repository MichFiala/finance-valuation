using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.SavingsForecast.Get.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.SavingsForecast.Get;

internal class GetSavingsForecastEndpoint(UserManager<User.Models.User> userManager, SavingsForecastService savingsForecastService)
: Endpoint<GetSavingsForecastRequest, GetSavingsForecastResponse>
{
    public override void Configure()
    {
        Get("/savings-forecast");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Returns when you hit target savings";
            s.Description = "Calculates the duration to your target savings";
        });
    }

    public override async Task HandleAsync(GetSavingsForecastRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        (int forecastedMonths, List<SavingsForecastStepDto> forecastSteps) = await savingsForecastService.CalculateForecastAsync(user.Id, request.SavingId, request.MainIncomeStrategyId, request.SideIncomeStrategyId);

        await Send.OkAsync(new GetSavingsForecastResponse
        {
            Months = forecastedMonths,
            Date = DateOnly.FromDateTime(DateTime.Now.AddMonths(forecastedMonths)),
            ForecastSteps = forecastSteps
        }, ct);
    }
}

