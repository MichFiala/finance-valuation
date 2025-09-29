using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Strategies.Get.Models;
using Finances.Valuation.Application.Features.Strategies.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Get;

internal class GetStrategiesEndpoint(UserManager<User.Models.User> userManager, StrategyRepository strategyRepository)
    : EndpointWithoutRequest<GetStrategiesResponse>
{
    public override void Configure()
    {
        Get("/strategies");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets strategies";
            s.Description = "Returns strategies DTOs";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        IReadOnlyCollection<Strategy>? strategies = await strategyRepository.GetAsync(user.Id);

        var dtos = strategies.Select(StrategyDto.Create).ToList();

        await Send.OkAsync(new GetStrategiesResponse
        {
            Strategies = dtos
        }, ct);
    }
}