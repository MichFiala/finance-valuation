using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Finances.Valuation.Application.Features.Authentication;

internal class GoogleAuthenticationEndpoint : EndpointWithoutRequest<ChallengeHttpResult>
{
    public override void Configure()
    {
        Get("/login/google");
        AllowAnonymous();
    }

    public override async Task<ChallengeHttpResult> ExecuteAsync(CancellationToken ct)
    {
        string returnUrl = Query<string>("returnUrl")!;

        var props = new AuthenticationProperties
        {
            RedirectUri = $"/login/google/callback?returnUrl={returnUrl}"
        };

        await Task.CompletedTask;
        return TypedResults.Challenge(props, ["Google"]);
    }
}
