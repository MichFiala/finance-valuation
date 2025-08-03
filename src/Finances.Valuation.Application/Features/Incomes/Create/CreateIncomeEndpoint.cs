using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Models;

namespace Finances.Valuation.Application.Features.Incomes.Create;

internal class CreateIncomeEndpoint(IncomeRepository incomeRepository) : Endpoint<IncomeDto, IncomeDto>
{
    public override void Configure()
    {
        Post("/incomes");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new income entry";
            s.Description = "Creates a new income entry";
        });
    }

    public override async Task<IncomeDto> HandleAsync(IncomeDto incomeDto, CancellationToken ct)
    {
        var income = new Income
        {
            Name = incomeDto.Name,
            Amount = incomeDto.Amount,
            Date = incomeDto.Date
        };

        await incomeRepository.SaveAsync(income);

        return incomeDto;
    }
}
