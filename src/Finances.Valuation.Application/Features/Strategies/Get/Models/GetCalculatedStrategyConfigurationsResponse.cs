using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get.Models;

public class GetCalculatedStrategyConfigurationsResponse
{
    public IReadOnlyCollection<StrategyConfigurationCalculationStepDto> StrategyConfigurationsCalculationSteps { get; set; } = [];
}
