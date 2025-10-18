using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Incomes.Models;

public class IncomeDto : IEntityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public bool IsMainIncome { get; set; }

    internal static IncomeDto Create(Income income)
    {
        return new IncomeDto
        {
            Id = income.Id,
            Name = income.Name,
            Amount = income.Amount,
            Date = income.Date,
            IsMainIncome = income.IsMainIncome
        };
    }
}
