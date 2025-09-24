using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.User.Get.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.User.Get;

internal class MeEndpoint(UserManager<User.Models.User> userManager) : EndpointWithoutRequest<Results<Ok<MeResponse>, RedirectHttpResult>>
{
    public override void Configure()
    {
        Get("/users/me");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets actual user info";
            s.Description = "Returns the user name of actual user";
        });
    }

    public override async Task<Results<Ok<MeResponse>, RedirectHttpResult>> ExecuteAsync(CancellationToken ct)
    {
        await Task.CompletedTask;
        try
        {
            User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email());

            return TypedResults.Ok(new MeResponse
            {
                UserName = user.Email,
                Image = user.Image
            });
        }
        catch (Exception)
        {
            return TypedResults.Redirect("/login/google");
        }
    }
}

