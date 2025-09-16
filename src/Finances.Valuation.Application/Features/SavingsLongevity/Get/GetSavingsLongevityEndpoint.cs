using FastEndpoints;
using Finances.Valuation.Application.Features.SavingsLongevity.Get.Models;

namespace Finances.Valuation.Application.Features.SavingsLongevity.Get;

internal class GetSavingsLongevityEndpoint(SavingsLongevityCalculationService longevityService)
    : Endpoint<EmptyRequest, GetSavingsLongevityResponse>
{
    public override void Configure()
    {
        Get("/savings-longevity");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Returns how many months your savings and investments will last based on spendings.";
            s.Description = "Calculates the duration your savings and investments can cover your spendings.";
        });
    }

    public override async Task HandleAsync(EmptyRequest emptyRequest, CancellationToken ct)
    {
        int longevity = await longevityService.CalculateMonthsOfLongevityAsync();

        var grade = SavingsLongevityCalculationService.ValuateGrade(longevity);

        await Send.OkAsync(new GetSavingsLongevityResponse
        {
            Months = longevity,
            Till = DateOnly.FromDateTime(DateTime.Now.AddMonths(longevity)),
            Grade = grade
        }, ct);
    }
}