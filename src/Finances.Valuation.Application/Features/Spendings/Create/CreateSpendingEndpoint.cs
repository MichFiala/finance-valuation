using FastEndpoints;
using Finances.Valuation.Application.Features.Spendings.Create.Models;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Create;

internal class CreateSpendingEndpoint(SpendingRepository spendingRepository) : Endpoint<CreateSpendingRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/spendings");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new spending entry";
            s.Description = "Creates a new spending entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CreateSpendingRequest request, CancellationToken ct)
    {
        SpendingDto dto = new()
        {
            Name = request.Name,
            Amount = request.Amount,
            Frequency = request.Frequency,
            IsMandatory = request.IsMandatory
        };

        var spending = Spending.Create(dto);

        await spendingRepository.SaveAsync(spending);

        return new EmptyResponse();
    }
}
