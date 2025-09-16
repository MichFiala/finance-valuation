using FastEndpoints;
using Finances.Valuation.Application.Features.Spendings.Get.Models;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Get;

internal class GetSpendingsEndpoint(SpendingRepository spendingRepository) : Endpoint<EmptyRequest, GetSpendingsResponse>
{
    public override void Configure()
    {
        Get("/spendings");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets spendings";
            s.Description = "Returns the spending DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        var spendings = await spendingRepository.GetAsync();

        if(spendings is null)
            ThrowError("Spendings not found.");

        await Send.OkAsync(new GetSpendingsResponse
        {
            Spendings = spendings.Select(SpendingDto.Create).ToList()
        }, ct);
    }
}