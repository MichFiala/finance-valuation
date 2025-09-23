using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Endpoints.Create;

internal class CreateStrategyEndpoint(UserManager<User.Models.User> userManager, StrategyRepository strategyRepository) 
    : Endpoint<StrategyDto, StrategyDto>
{
    public override void Configure()
    {
        Post("/strategies");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new strategy";
            s.Description = "Creates a new strategy with configurations";
        });
    }

    public override async Task<StrategyDto> HandleAsync(StrategyDto strategyDto, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        Strategy strategy = Strategy.Create(strategyDto, user.Id);
        strategy = await strategyRepository.SaveAsync(strategy);

        List<StrategyConfiguration> strategyConfigurations =
            strategyDto.StrategyConfigurations.Select((conf, i) => StrategyConfiguration.Create(strategy, conf, i))
                                              .ToList();

        await strategyRepository.SaveAsync(strategyConfigurations);

        return strategyDto;
    }
}