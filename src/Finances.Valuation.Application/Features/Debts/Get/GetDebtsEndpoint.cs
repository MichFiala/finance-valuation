using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Get.Models;
using Finances.Valuation.Application.Features.Debts.Models;

namespace Finances.Valuation.Application.Features.Debts.Get;

internal class GetDebtsEndpoint(DebtRepository debtRepository) : Endpoint<EmptyRequest, GetDebtsResponse>
{
    public override void Configure()
    {
        Get("/debts");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets all debts";
            s.Description = "Returns a list of all debt DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        IReadOnlyCollection<Debt>? debts = await debtRepository.GetAsync();

        var debtDtos = debts.Select(DebtDto.Create).ToList();

        await Send.OkAsync(new GetDebtsResponse
        {
            Debts = debtDtos
        }, ct);
    }
}
