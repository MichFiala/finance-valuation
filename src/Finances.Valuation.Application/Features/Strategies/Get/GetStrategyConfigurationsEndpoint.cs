using FastEndpoints;
using Finances.Valuation.Application.Features.Strategies.Get.Models;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get;

internal class GetStrategyConfigurationsEndpoint(StrategyRepository strategyRepository) 
    : Endpoint<GetStrategyConfigurationsRequest, GetStrategyConfigurationsResponse>
{
    public override void Configure()
    {
        Get("/strategies/{StrategyId}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Gets a single strategy configuration by id";
            s.Description = "Returns the strategy configuration DTO for the given id";
        });
    }

    public override async Task HandleAsync(GetStrategyConfigurationsRequest request, CancellationToken ct)
    {
        var strategy = await strategyRepository.GetAsync(request.StrategyId);

        if(strategy is null)
            ThrowError("Strategy not found for {request.StrategyId}.");

        IReadOnlyCollection<StrategyConfiguration>? strategyConfigurations = await strategyRepository.GetByStrategyIdAsync(request.StrategyId);

        if (strategyConfigurations is null)
            ThrowError("Strategy configurations not found for {request.StrategyId}.");

        var strategyConfigurationsDtos = strategyConfigurations.Select(StrategyConfigurationDto.Create).ToList();

        await Send.OkAsync(new GetStrategyConfigurationsResponse
        {
            Name = strategy.Name,
            StrategyConfigurations = strategyConfigurationsDtos
        }, ct);
    }
}
