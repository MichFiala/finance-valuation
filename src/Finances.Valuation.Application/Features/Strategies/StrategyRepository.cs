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


        var strategies = strategyConfigurations.Select(conf => conf.StrategyId).ToList();

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

