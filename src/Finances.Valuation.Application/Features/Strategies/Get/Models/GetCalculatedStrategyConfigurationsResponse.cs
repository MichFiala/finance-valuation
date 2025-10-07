using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get.Models;

public class GetCalculatedStrategyConfigurationsResponse
{
    public required string Name { get; set; }
    public IReadOnlyCollection<StrategyConfigurationCalculationStepDto> StrategyConfigurationsCalculationSteps { get; set; } = [];
    public IReadOnlyCollection<StrategyConfigurationCalculationByAccountDto> StrategyConfigurationsCalculationByAccounts { get; set; } = [];
}
