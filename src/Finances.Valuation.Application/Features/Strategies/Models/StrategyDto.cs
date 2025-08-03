namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyDto
{
    public required string Name { get; set; }    

    public required IReadOnlyCollection<StrategyConfigurationDto> StrategyConfigurations { get; set; }
}
