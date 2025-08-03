using FastEndpoints;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Create;

internal class CreateSpendingEndpoint(SpendingRepository spendingRepository) : Endpoint<SpendingDto, SpendingDto>
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

    public override async Task<SpendingDto> HandleAsync(SpendingDto spendingDto, CancellationToken ct)
    {
        var spending = new Spending
        {
            Name = spendingDto.Name,
            Amount = spendingDto.Amount,
            Frequency = spendingDto.Frequency
        };

        await spendingRepository.SaveAsync(spending);

        return spendingDto;
    }
}
