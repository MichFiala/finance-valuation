using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Update;

internal class UpdateStrategyEndpoint(UpdateHandler updateHandler, StrategyRepository strategyRepository, UserManager<User.Models.User> userManager) : Endpoint<StrategyDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/strategies/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing strategy entry";
            s.Description = "Updates an existing strategy entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(StrategyDto request, CancellationToken ct)
    {
        var response = await updateHandler.HandleAsync(request, (strategy) =>
        {
            strategy.Name = request.Name;
        }, strategyRepository, HttpContext);

        User.Models.User user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new System.Security.Authentication.AuthenticationException($"User not found by email {HttpContext.Email()}");

        List<StrategyConfiguration> strategyConfigurations =
            request.StrategyConfigurations.Select((conf, i) => StrategyConfiguration.Create(request.Id, user.Id , conf, i))
                                          .ToList();

        await strategyRepository.SaveAsync(strategyConfigurations);

        return response;
    }
}
