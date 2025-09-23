using System.Text.Json.Serialization;
using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Spendings.Models;

public class SpendingDto : IEntityDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; }

    public bool IsMandatory { get; set; }
}
