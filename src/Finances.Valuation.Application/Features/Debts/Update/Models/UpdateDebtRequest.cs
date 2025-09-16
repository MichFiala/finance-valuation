namespace Finances.Valuation.Application.Features.Debts.Update.Models;

public class UpdateDebtRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Amount { get; set; }
}
