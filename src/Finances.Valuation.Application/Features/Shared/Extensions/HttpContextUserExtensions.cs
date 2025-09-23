using System.Security.Claims;

namespace Finances.Valuation.Application.Features.Shared.Extensions;

internal static class HttpContextUserExtensions
{
    public static string Email(this HttpContext context)
    {
        var user = context.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            throw new Exception("User identity not found");

        string? email = user.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            throw new Exception($"User {nameof(ClaimTypes.Email)} not set");

        return email;
    } 
}
