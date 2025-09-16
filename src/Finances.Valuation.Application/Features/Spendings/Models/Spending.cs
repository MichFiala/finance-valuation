namespace Finances.Valuation.Application.Features.Spendings.Models;

internal class Spending
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public Frequency Frequency { get; set; }

    public bool IsMandatory { get; set; }

    internal static Spending Create(SpendingDto spendingDto)
    {
        return new Spending
        {
            Name = spendingDto.Name,
            Amount = spendingDto.Amount,
            Frequency = spendingDto.Frequency,
            IsMandatory = spendingDto.IsMandatory
        };
    }
}
