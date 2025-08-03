using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Spendings.Models;

public class SpendingDto
{
    public required string Name { get; set; }

    public decimal Amount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; }
}
