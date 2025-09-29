using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Shared.Repositories;
using Finances.Valuation.Application.Features.Strategies.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Strategies;

internal class StrategyRepository(IDbContextFactory<AppDbContext> dbContextFactory, CrudDomainRepository crudDomainRepository) : ICrudDomainRepository<Strategy>
{
    public async Task<IReadOnlyCollection<Strategy>> GetAsync(string userId) => await crudDomainRepository.GetAsync<Strategy>(userId);

    public async Task<Strategy?> GetAsync(int id, string userId) => await crudDomainRepository.GetAsync<Strategy>(id, userId);

    public async Task<IReadOnlyCollection<StrategyConfiguration>> GetByStrategyIdAsync(int strategyId, string userId)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        var debtStrategyConfigurations =
            await context.StrategiesConfigurations
                         .OfUser(userId)
                         .Where(conf => conf.DebtId != null)
                         .Include(conf => conf.Debt)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var savingStrategyConfigurations =
            await context.StrategiesConfigurations
                         .OfUser(userId)
                         .Where(conf => conf.SavingId != null)
                         .Include(conf => conf.Saving)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var spendingStrategyConfigurations =
            await context.StrategiesConfigurations
                         .OfUser(userId)
                         .Where(conf => conf.SpendingId != null)
                         .Include(conf => conf.Spending)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var investmentStrategyConfigurations =
            await context.StrategiesConfigurations
                         .OfUser(userId)
                         .Where(conf => conf.InvestmentId != null)
                         .Include(conf => conf.Investment)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        return debtStrategyConfigurations.Concat(savingStrategyConfigurations)
                                         .Concat(spendingStrategyConfigurations)
                                         .Concat(investmentStrategyConfigurations)
                                         .OrderBy(conf => conf.Priority)
                                         .ToList();
    }

    public async Task<Strategy> SaveAsync(Strategy strategy) => await crudDomainRepository.SaveAsync(strategy, s => s.SetProperty(s => s.Name, s => strategy.Name));

    public async Task SaveAsync(IReadOnlyCollection<StrategyConfiguration> strategyConfigurations)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        if (strategyConfigurations.Count == 0)
            return;


        var strategies = strategyConfigurations.Select(conf => conf.StrategyId).Distinct().ToList();

        if (strategies.Count != 1)
            throw new InvalidDataException("Only configuration for one strategy allowed");

        int strategyId = strategies[0];

        List<StrategyConfiguration> existingStrategyConfigurations = await context.StrategiesConfigurations.Where(conf => conf.StrategyId == strategyId).ToListAsync();

        if (existingStrategyConfigurations.Count > 0)
        {
            context.StrategiesConfigurations.RemoveRange(existingStrategyConfigurations);
        }

        context.StrategiesConfigurations.AddRange(strategyConfigurations);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int strategyId, string userId)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        await context.StrategiesConfigurations.OfUser(userId).Where(d => d.StrategyId == strategyId).ExecuteDeleteAsync();

        await CrudDomainRepository.DeleteAsync<Strategy>(strategyId, userId, context);
    }
}

