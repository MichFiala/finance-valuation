using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Spendings.Models;

public class SpendingDto
{
    public required string Name { get; set; }

    public decimal Amount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; }
    
    internal static SpendingDto Create(Spending spending)
    {
        return new SpendingDto
        {
            Name = spending.Name,
            Amount = spending.Amount,
            Frequency = spending.Frequency
        };
    }
}
