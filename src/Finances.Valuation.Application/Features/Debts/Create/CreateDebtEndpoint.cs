using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Models;

namespace Finances.Valuation.Application.Features.Debts.Create;

internal class CreateDebtEndpoint(DebtRepository debtRepository) : Endpoint<DebtDto, DebtDto>
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

    public override async Task<DebtDto> HandleAsync(DebtDto debtDto, CancellationToken ct)
    {
        var debt = new Debt
        {
            Name = debtDto.Name,
            DebtType = debtDto.DebtType,
            Amount = debtDto.Amount,
            Interest = debtDto.Interest,
            Payment = debtDto.Payment
        };

        await debtRepository.SaveAsync(debt);

        return DebtDto.Create(debt);
    }
}