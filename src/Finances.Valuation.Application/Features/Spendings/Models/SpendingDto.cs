using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Spendings.Models;

public class SpendingDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; }

    public bool IsMandatory { get; set; }
    
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
