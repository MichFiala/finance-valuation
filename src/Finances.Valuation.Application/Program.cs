using Finances.Valuation.Application.Features.Debt;
using Finances.Valuation.Application.Features.Income;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:SQLiteDefault"]), ServiceLifetime.Scoped);

builder.Services.AddTransient<DebtRepository>();
builder.Services.AddTransient<IncomeRepository>();
builder.Services.AddTransient<SavingRepository>();
builder.Services.AddTransient<SpendingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();