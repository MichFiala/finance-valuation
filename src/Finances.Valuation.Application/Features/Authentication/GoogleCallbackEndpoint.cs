using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Authentication;

internal class GoogleCallbackEndpoint(UserManager<User.Models.User> userManager, SignInManager<User.Models.User> signInManager) : EndpointWithoutRequest<RedirectHttpResult>
{
    public override void Configure()
    {
        Get("/login/google/callback");
        AllowAnonymous();
    }

    public override async Task<RedirectHttpResult> ExecuteAsync(CancellationToken ct)
    {
        string returnUrl = Query<string>("returnUrl")!;

        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        var email = result.Principal?.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        var imageUrl = result.Principal?.FindFirst("urn:google:image")?.Value;

        var user = await userManager.FindByEmailAsync(email!);
        if (user == null)
        {
            user = new User.Models.User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                Image = imageUrl
            };
            await userManager.CreateAsync(user);
        }
        else if (imageUrl is not null && user.Image is null)
        {
            user.Image = imageUrl;

            await userManager.UpdateAsync(user);
        }

        await signInManager.SignInAsync(user, isPersistent: false);

        return TypedResults.Redirect(returnUrl);
    }
}
