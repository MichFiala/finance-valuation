using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.SavingsForecast.Get.Models;

public class SavingsForecastStepDto
{
    public required IncomeDto IncomeDto { get; set; }
    public decimal CurrentAmount { get; set; }
    public decimal TargetAmountDifference { get; set; }
    public decimal ContributedAmount { get; set; }
    public required List<StrategyConfigurationCalculationStepDto>? StrategyCalculationSteps { get; set; }
}
