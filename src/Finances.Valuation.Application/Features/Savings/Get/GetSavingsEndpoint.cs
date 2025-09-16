using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Get.Models;
using Finances.Valuation.Application.Features.Savings.Models;

namespace Finances.Valuation.Application.Features.Savings.Get;

internal class GetSavingsEndpoint(SavingRepository savingRepository) : Endpoint<EmptyRequest, GetSavingsResponse>
{
    public override void Configure()
    {
        Get("/savings");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets all savings";
            s.Description = "Returns a list of all savings DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        IReadOnlyCollection<Saving>? savings = await savingRepository.GetAsync();

        var savingDtos = savings.Select(SavingDto.Create).ToList();

        await Send.OkAsync(new GetSavingsResponse
        {
            Savings = savingDtos
        }, ct);
    }
}

