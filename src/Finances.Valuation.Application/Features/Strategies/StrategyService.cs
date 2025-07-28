using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies;

public interface IStrategyService
{
    Task CreateAsync(StrategyDto strategyDto);
}

internal class StrategyService(StrategyRepository strategyRepository) : IStrategyService
{
    public async Task CreateAsync(StrategyDto strategyDto)
    {
        var strategy = new Strategy
        {
            Name = strategyDto.Name,
        };

        int strategyId = await strategyRepository.SaveAsync(strategy);

        List<StrategyConfiguration> strategyConfigurations =
            strategyDto.StrategyConfigurations.Select(conf => StrategyConfiguration.Create(strategyId, conf.Type, conf.ReferenceId))
                                              .ToList();

        await strategyRepository.SaveAsync(strategyConfigurations);
    }
}
