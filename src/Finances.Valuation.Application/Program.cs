using FastEndpoints;
using FastEndpoints.Swagger; 
using Finances.Valuation.Application.Features.Debts;
using Finances.Valuation.Application.Features.Incomes;
using Finances.Valuation.Application.Features.Investments;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.SavingsLongevity;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Features.Strategies;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddFastEndpoints()
                .SwaggerDocument();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:SQLiteDefault"]), ServiceLifetime.Scoped);

builder.Services.AddTransient<DebtRepository>();
builder.Services.AddTransient<IncomeRepository>();
builder.Services.AddTransient<SavingRepository>();
builder.Services.AddTransient<SpendingRepository>();
builder.Services.AddTransient<StrategyRepository>();
builder.Services.AddTransient<InvestmentRepository>();

builder.Services.AddTransient<SavingsLongevityCalculationService>();

var app = builder.Build();

app.UseFastEndpoints()
   .UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();