using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Incomes.Models;

internal class Income : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    internal static Income Create(IncomeDto incomeDto, string userId)
    {
        return new Income
        {
            Name = incomeDto.Name,
            Amount = incomeDto.Amount,
            Date = incomeDto.Date,
            UserId = userId,
        };
    }
}
