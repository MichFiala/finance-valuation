using System.Text.Json.Serialization;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Update.Models;

public class UpdateSpendingRequest
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Amount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; }
    
    public bool IsMandatory { get; set; }
}
