using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Debts.Models;

public class DebtDto
{
    public int Id { get; set;}
    public required string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DebtType DebtType { get; set; }

    public decimal Amount { get; set; }

    public decimal Interest { get; set; }
    
    public decimal Payment { get; set; }

    internal static DebtDto Create(Debt debt)
    {
        return new DebtDto
        {
            Id = debt.Id,
            Name = debt.Name,
            DebtType = debt.DebtType,
            Amount = debt.Amount,
            Interest = debt.Interest,
            Payment = debt.Payment
        };
    }
}