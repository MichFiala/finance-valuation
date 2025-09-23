
using System.Runtime.CompilerServices;
using Finances.Valuation.Application.Features.Shared.Models;

[assembly: InternalsVisibleTo("Finances.Valuation.Application.Test")]

namespace Finances.Valuation.Application.Features.Savings.Models;

internal class Saving : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Amount { get; set; }

    public decimal? TargetAmount { get; set; }

    public decimal? ExpectedMonthlyContributionAmount { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    internal static Saving Create(SavingDto savingDto, string userId)
    {
        return new Saving
        {
            Name = savingDto.Name,
            Amount = savingDto.Amount,
            TargetAmount = savingDto.TargetAmount,
            ExpectedMonthlyContributionAmount = savingDto.ExpectedMonthlyContributionAmount,
            UserId = userId
        };
    }
}

