using System.Runtime.CompilerServices;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Models;

[assembly: InternalsVisibleTo("Finances.Valuation.Application.Test")]

namespace Finances.Valuation.Application.Features.Debts.Models;

internal class Debt : IDatabaseEntry, IUserRelated
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DebtType DebtType { get; set; }

    public decimal Amount { get; set; }

    public decimal Interest { get; set; }

    public decimal Payment { get; set; }

    public Saving? Saving { get; set; } = null;

    public int? SavingId { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    internal static Debt Create(DebtDto debtDto, string userId)
    {
        return new Debt
        {
            Name = debtDto.Name,
            DebtType = debtDto.DebtType,
            Amount = debtDto.Amount,
            Interest = debtDto.Interest,
            Payment = debtDto.Payment,
            UserId = userId
        };
    }
}
