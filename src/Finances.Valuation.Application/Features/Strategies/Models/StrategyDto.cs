using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyDto : IEntityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required IReadOnlyCollection<StrategyConfigurationDto> StrategyConfigurations { get; set; }

    internal static StrategyDto Create(Strategy strategy) =>
        new()
        {
            Id = strategy.Id,
            Name = strategy.Name,
            StrategyConfigurations = Enumerable.Empty<StrategyConfigurationDto>().ToList()
        };
}
