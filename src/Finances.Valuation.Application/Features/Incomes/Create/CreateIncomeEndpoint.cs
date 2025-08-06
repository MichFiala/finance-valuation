using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Models;

namespace Finances.Valuation.Application.Features.Incomes.Create;

internal class CreateIncomeEndpoint(IncomeRepository incomeRepository) : Endpoint<List<IncomeDto>, List<IncomeDto>>
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

    public override async Task<List<IncomeDto>> HandleAsync(List<IncomeDto> incomeDtos, CancellationToken ct)
    {
        if (incomeDtos == null || incomeDtos.Count == 0)
        {
            throw new ArgumentException("IncomeDto list cannot be null or empty", nameof(incomeDtos));
        }

        var incomes = new List<Income>();

        foreach (var incomeDto in incomeDtos)
        {
            var income = Income.Create(incomeDto);

            await incomeRepository.SaveAsync(income);

            incomes.Add(income);
        }
        
        return incomes.Select(IncomeDto.Create).ToList();
    }
}
