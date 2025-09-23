using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Investments.Models;

internal class Investment : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }    

    internal static Investment Create(InvestmentDto investmentDto, string userId)
    {
        return new Investment
        {
            Name = investmentDto.Name,
            Amount = investmentDto.Amount,
            UserId = userId,
        };
    }
}
