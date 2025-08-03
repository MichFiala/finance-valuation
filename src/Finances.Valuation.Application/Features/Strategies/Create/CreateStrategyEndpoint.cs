using FastEndpoints;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Endpoints.Create;

internal class CreateStrategyEndpoint(StrategyRepository strategyRepository) 
    : Endpoint<StrategyDto, StrategyDto>
{
    public override void Configure()
    {
        Post("/strategies");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new strategy";
            s.Description = "Creates a new strategy with configurations";
        });
    }

    public override async Task<StrategyDto> HandleAsync(StrategyDto strategyDto, CancellationToken ct)
    {
        var strategy = new Strategy
        {
            Name = strategyDto.Name,
        };

        strategy = await strategyRepository.SaveAsync(strategy);

        List<StrategyConfiguration> strategyConfigurations =
            strategyDto.StrategyConfigurations.Select((conf, i) => StrategyConfiguration.Create(strategy, conf, i))
                                              .ToList();

        await strategyRepository.SaveAsync(strategyConfigurations);

        return strategyDto;
    }
}