using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.User.Models;

internal class User : IdentityUser
{
    public string? Image { get; set; }
}
