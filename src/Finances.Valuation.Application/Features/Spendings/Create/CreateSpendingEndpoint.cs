using FastEndpoints;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Create;

internal class CreateSpendingEndpoint(SpendingRepository spendingRepository) : Endpoint<List<SpendingDto>, List<SpendingDto>>
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

    public override async Task<List<SpendingDto>> HandleAsync(List<SpendingDto> spendingDtos, CancellationToken ct)
    {
        if (spendingDtos == null || spendingDtos.Count == 0)
        {
            throw new ArgumentException("SpendingDto list cannot be null or empty", nameof(spendingDtos));
        }

        var spendings = new List<Spending>();

        foreach (var spendingDto in spendingDtos)
        {
            var spending = Spending.Create(spendingDto);

            await spendingRepository.SaveAsync(spending);
            
            spendings.Add(spending);
        }

        return spendings.Select(SpendingDto.Create).ToList();
    }
}
