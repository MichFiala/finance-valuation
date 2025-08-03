namespace Finances.Valuation.Application.Features.Incomes.Models;

public class IncomeDto
{
    public required string Name { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }

    internal static IncomeDto Create(Income income)
    {
        return new IncomeDto
        {
            Name = income.Name,
            Amount = income.Amount,
            Date = income.Date
        };
    }
}
