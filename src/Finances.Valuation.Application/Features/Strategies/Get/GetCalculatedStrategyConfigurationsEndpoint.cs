using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Strategies.Get.Models;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Get;

internal class GetCalculatedStrategyConfigurationsEndpoint(UserManager<User.Models.User> userManager, StrategyRepository strategyRepository, IncomeRepository incomeRepository) 
    : Endpoint<GetCalculatedStrategyConfigurationsRequest, GetCalculatedStrategyConfigurationsResponse>
{
    public override void Configure()
    {
        Get("/strategies/{StrategyId}/calculate");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Calculates the strategy configurations";
            s.Description = "Returns the calculated strategy configurations for the given strategy id";
        });
    }

    public override async Task HandleAsync(GetCalculatedStrategyConfigurationsRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        Strategy? strategy = await strategyRepository.GetAsync(request.StrategyId, user.Id);
        
        if (strategy is null)
            ThrowError("Strategy not found for {request.StrategyId}.");

        IReadOnlyCollection<StrategyConfiguration>? strategyConfigurations = await strategyRepository.GetByStrategyIdAsync(request.StrategyId, user.Id);

        if (strategyConfigurations is null)
            ThrowError("Strategy configurations not found for {request.StrategyId}.");

        var income = await incomeRepository.GetAsync(request.IncomeId, user.Id);

        if (income is null)
            ThrowError("Income not found for {request.IncomeId}.");

        IReadOnlyList<StrategyConfigurationDto> strategyConfigurationsDtos = strategyConfigurations.Select(StrategyConfigurationDto.Create).ToList();

        IEnumerable<StrategyConfigurationCalculationStepDto> calculatedConfigurations = StrategyConfigurationsCalculationService.Calculate(strategyConfigurationsDtos, income);
        
        await Send.OkAsync(new GetCalculatedStrategyConfigurationsResponse
        {
            Name = strategy.Name,
            StrategyConfigurationsCalculationSteps = calculatedConfigurations.ToList()
        }, ct);
    }
}
