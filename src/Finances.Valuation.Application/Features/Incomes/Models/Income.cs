namespace Finances.Valuation.Application.Features.Incomes.Models;

internal class Income
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    internal static Income Create(IncomeDto incomeDto)
    {
        return new Income
        {
            Name = incomeDto.Name,
            Amount = incomeDto.Amount,
            Date = incomeDto.Date
        };
    }
}
