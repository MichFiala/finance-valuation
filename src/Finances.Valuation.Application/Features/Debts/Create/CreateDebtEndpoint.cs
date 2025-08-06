using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Models;

namespace Finances.Valuation.Application.Features.Debts.Create;

internal class CreateDebtEndpoint(DebtRepository debtRepository) : Endpoint<List<DebtDto>, List<DebtDto>>
{
    public override void Configure()
    {
        Post("/debts");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new debt";
            s.Description = "Creates a new debt entry";
        });
    }

    public override async Task<IReadOnlyCollection<DebtDto>> HandleAsync(List<DebtDto> debtDtos, CancellationToken ct)
    {
        if (debtDtos == null || debtDtos.Count == 0)
        {
            throw new ArgumentException("DebtDto list cannot be null or empty", nameof(debtDtos));
        }

        var debts = new List<Debt>();

        foreach (var debtDto in debtDtos)
        {
            var debt = Debt.Create(debtDto);
            await debtRepository.SaveAsync(debt);
            debts.Add(debt);
        }

        return debts.Select(DebtDto.Create).ToList();
    }
}