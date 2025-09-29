
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get.Models;

public class GetStrategyConfigurationsResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<StrategyConfigurationDto> StrategyConfigurations { get; set; } = [];
}

