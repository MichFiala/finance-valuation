using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Spendings.Models;

internal class Spending : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public Frequency Frequency { get; set; }

    public bool IsMandatory { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    internal static Spending Create(SpendingDto spendingDto, string userId)
    {
        return new Spending
        {
            Name = spendingDto.Name,
            Amount = spendingDto.Amount,
            Frequency = spendingDto.Frequency,
            IsMandatory = spendingDto.IsMandatory,
            UserId = userId
        };
    }
    
    internal static SpendingDto Create(Spending spending)
    {
        return new SpendingDto
        {
            Id = spending.Id,
            Name = spending.Name,
            Amount = spending.Amount,
            Frequency = spending.Frequency,
            IsMandatory = spending.IsMandatory
        };
    }
}
