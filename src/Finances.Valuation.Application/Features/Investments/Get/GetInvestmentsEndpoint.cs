using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Get.Models;
using Finances.Valuation.Application.Features.Investments.Get.Models;
using Finances.Valuation.Application.Features.Investments.Models;

namespace Finances.Valuation.Application.Features.Investments.Get;

internal class GetInvestmentsEndpoint(InvestmentRepository investmentRepository) : Endpoint<EmptyRequest, GetInvestmentsResponse>
{
    public override void Configure()
    {
        Get("/investments");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets all investments";
            s.Description = "Returns a list of all investment DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        IReadOnlyCollection<Investment>? investments = await investmentRepository.GetAsync();

        var investmentDtos = investments.Select(InvestmentDto.Create).ToList();

        await Send.OkAsync(new GetInvestmentsResponse
        {
            Investments = investmentDtos
        }, ct);
    }
}
