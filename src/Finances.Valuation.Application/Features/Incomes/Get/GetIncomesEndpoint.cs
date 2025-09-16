using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Get.Models;
using Finances.Valuation.Application.Features.Incomes.Models;

namespace Finances.Valuation.Application.Features.Incomes.Get;

internal class GetIncomesEndpoint(IncomeRepository incomeRepository) 
    : Endpoint<EmptyRequest, GetIncomesResponse>
{
    public override void Configure()
    {
        Get("/incomes");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets incomes";
            s.Description = "Returns the income DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        var incomes = await incomeRepository.GetAsync();

        if(incomes is null)
            ThrowError("Incomes not found.");

        await Send.OkAsync(new GetIncomesResponse
        {
            Incomes = incomes.Select(IncomeDto.Create).ToList()
        }, ct);
    }
}
