using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get.Models;

public class GetStrategiesResponse
{
    public IReadOnlyCollection<StrategyDto> Strategies { get; set; } = [];     
}
