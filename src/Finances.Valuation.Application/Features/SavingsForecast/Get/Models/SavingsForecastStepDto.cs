using Finances.Valuation.Application.Features.Incomes.Models;

namespace Finances.Valuation.Application.Features.SavingsForecast.Get.Models;

public class SavingsForecastStepDto
{
    public required IncomeDto IncomeDto { get; set; }
    public decimal CurrentAmount { get; set; }
    public decimal TargetAmountDifference { get; set; } 
    public decimal ContributedAmount { get; set; }
}
