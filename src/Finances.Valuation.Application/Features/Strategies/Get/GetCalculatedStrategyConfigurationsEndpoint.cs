using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Strategies.Get.Models;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies.Get;

internal class GetCalculatedStrategyConfigurationsEndpoint(StrategyRepository strategyRepository, IncomeRepository incomeRepository) 
    : Endpoint<GetCalculatedStrategyConfigurationsRequest, GetCalculatedStrategyConfigurationsResponse>
{
    public override void Configure()
    {
        Get("/strategies/{StrategyId}/calculate");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Calculates the strategy configurations";
            s.Description = "Returns the calculated strategy configurations for the given strategy id";
        });
    }

    public override async Task HandleAsync(GetCalculatedStrategyConfigurationsRequest request, CancellationToken ct)
    {
        IReadOnlyCollection<StrategyConfiguration>? strategyConfigurations = await strategyRepository.GetByStrategyIdAsync(request.StrategyId);

        if (strategyConfigurations is null)
            ThrowError("Strategy configurations not found for {request.StrategyId}.");

        var income = await incomeRepository.GetAsync(request.IncomeId);

        if (income is null)
            ThrowError("Income not found for {request.IncomeId}.");

        IReadOnlyList<StrategyConfigurationDto> strategyConfigurationsDtos = strategyConfigurations.Select(StrategyConfigurationDto.Create).ToList();

        IEnumerable<StrategyConfigurationCalculationStepDto> calculatedConfigurations = StrategyConfigurationsCalculationService.Calculate(strategyConfigurationsDtos, income);
        
        await Send.OkAsync(new GetCalculatedStrategyConfigurationsResponse
        {
            StrategyConfigurationsCalculationSteps = calculatedConfigurations.ToList()
        }, ct);
    }
}
