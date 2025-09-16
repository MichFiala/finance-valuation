namespace Finances.Valuation.Application.Features.Investments.Update.Models;

public class UpdateInvestmentRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
}
