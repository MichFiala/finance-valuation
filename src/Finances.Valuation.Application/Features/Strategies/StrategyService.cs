using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies;

public interface IStrategyService
{
    Task<StrategyDto> CreateAsync(StrategyDto strategyDto);
}

internal class StrategyService(StrategyRepository strategyRepository) : IStrategyService
{
    public async Task<StrategyDto> CreateAsync(StrategyDto strategyDto)
    {
        var strategy = new Strategy
        {
            Name = strategyDto.Name,
        };

        strategy = await strategyRepository.SaveAsync(strategy);

        List<StrategyConfiguration> strategyConfigurations =
            strategyDto.StrategyConfigurations.Select(conf => StrategyConfiguration.Create(strategy.Id, conf.Type, conf.ReferenceId))
                                              .ToList();

        await strategyRepository.SaveAsync(strategyConfigurations);

        return new StrategyDto
        {
            Name = strategy.Name,
            StrategyConfigurations = strategyConfigurations.Select(conf => StrategyConfigurationDto.Create(conf)).ToList()
        };
    }
}
