using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Strategies.Models;

internal class Strategy : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    public static Strategy Create(StrategyDto strategyDto, string userId) =>
        new()
        {
            Name = strategyDto.Name,
            UserId = userId
        };
}
