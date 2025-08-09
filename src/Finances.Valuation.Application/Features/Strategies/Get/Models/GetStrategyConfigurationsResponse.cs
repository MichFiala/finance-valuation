
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get.Models;

public class GetStrategyConfigurationsResponse
{
    public List<StrategyConfigurationDto> StrategyConfigurations { get; set; } = [];
}

