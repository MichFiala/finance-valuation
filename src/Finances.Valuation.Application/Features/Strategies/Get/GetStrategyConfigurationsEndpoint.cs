using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Strategies.Get.Models;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Get;

internal class GetStrategyConfigurationsEndpoint(UserManager<User.Models.User> userManager, StrategyRepository strategyRepository) 
    : Endpoint<GetStrategyConfigurationsRequest, GetStrategyConfigurationsResponse>
{
    public override void Configure()
    {
        Get("/strategies/{StrategyId}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets a single strategy configuration by id";
            s.Description = "Returns the strategy configuration DTO for the given id";
        });
    }

    public override async Task HandleAsync(GetStrategyConfigurationsRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        var strategy = await strategyRepository.GetAsync(request.StrategyId, user.Id);

        if(strategy is null)
            ThrowError("Strategy not found for {request.StrategyId}.");

        IReadOnlyCollection<StrategyConfiguration>? strategyConfigurations = await strategyRepository.GetByStrategyIdAsync(request.StrategyId, user.Id);

        if (strategyConfigurations is null)
            ThrowError("Strategy configurations not found for {request.StrategyId}.");

        var strategyConfigurationsDtos = strategyConfigurations.Select(StrategyConfigurationDto.Create).ToList();

        await Send.OkAsync(new GetStrategyConfigurationsResponse
        {
            Id = strategy.Id,
            Name = strategy.Name,
            StrategyConfigurations = strategyConfigurationsDtos
        }, ct);
    }
}
