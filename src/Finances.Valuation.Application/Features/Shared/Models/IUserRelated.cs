namespace Finances.Valuation.Application.Features.Shared.Models;

internal interface IUserRelated
{
    public string UserId { get; set; }    

    public User.Models.User? User { get; set; }
}
