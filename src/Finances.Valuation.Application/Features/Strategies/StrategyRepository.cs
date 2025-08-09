using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finances.Valuation.Application.Features.Strategies;

internal class StrategyRepository(IDbContextFactory<AppDbContext> dbContextFactory)
{
    public async Task<IReadOnlyCollection<Models.Strategy>> GetAsync()
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        return await context.Strategies.ToListAsync();
    }

    public async Task<IReadOnlyCollection<Models.StrategyConfiguration>> GetByStrategyIdAsync(int strategyId)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        var debtStrategyConfigurations =
            await context.StrategiesConfigurations
                         .Where(conf => conf.DebtId != null)
                         .Include(conf => conf.Debt)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var savingStrategyConfigurations =
            await context.StrategiesConfigurations
                         .Where(conf => conf.SavingId != null)
                         .Include(conf => conf.Saving)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var spendingStrategyConfigurations =
            await context.StrategiesConfigurations
                         .Where(conf => conf.SpendingId != null)
                         .Include(conf => conf.Spending)
                         .Where(conf => conf.StrategyId == strategyId)
                         .ToListAsync();

        var investmentStrategyConfigurations =
            await context.StrategiesConfigurations
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

    public async Task<Models.Strategy> SaveAsync(Models.Strategy strategy)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();
        if (strategy.Id <= 0)
        {
            context.Strategies.Add(strategy);
            await context.SaveChangesAsync();

            return strategy;
        }

        await context.Strategies
                     .Where(s => s.Id == strategy.Id)
                     .ExecuteUpdateAsync(s =>
                        s.SetProperty(s => s.Name, s => strategy.Name)
                     );

        return strategy;
    }

    public async Task SaveAsync(IReadOnlyCollection<Models.StrategyConfiguration> strategyConfigurations)
    {
        using AppDbContext context = await dbContextFactory.CreateDbContextAsync();

        if (strategyConfigurations.Count == 0)
            return;


        var strategies = strategyConfigurations.Select(conf => conf.StrategyId).Distinct().ToList();

        if (strategies.Count != 1)
            throw new InvalidDataException("Only configuration for one strategy allowed");

        int strategyId = strategies[0];

        List<Models.StrategyConfiguration> existingStrategyConfigurations = await context.StrategiesConfigurations.Where(conf => conf.StrategyId == strategyId).ToListAsync();

        if (existingStrategyConfigurations.Count > 0)
        {
            context.StrategiesConfigurations.RemoveRange(existingStrategyConfigurations);
        }

        context.StrategiesConfigurations.AddRange(strategyConfigurations);

        await context.SaveChangesAsync();
    }
}

