using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Swagger;
using Finances.Valuation.Application.Features.Debts;
using Finances.Valuation.Application.Features.Incomes;
using Finances.Valuation.Application.Features.Investments;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.SavingsLongevity;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Finances.Valuation.Application.Features.Shared.Repositories;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Features.Strategies;
using Finances.Valuation.Application.Features.User.Models;
using Finances.Valuation.Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", options =>
        {
            options.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
        });
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = "Google";
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;    
})
.AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.Cookie.SameSite = SameSiteMode.None; // povolit cross-origin
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // nutné pro HTTPS
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.SignInScheme = IdentityConstants.ApplicationScheme;
});
builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();


builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:SQLiteDefault"]), ServiceLifetime.Scoped);
    
builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddApiEndpoints();

builder.Services.AddTransient<CreateHandler>();
builder.Services.AddTransient<DeleteHandler>();
builder.Services.AddTransient<UpdateHandler>();

builder.Services.AddTransient<CrudDomainRepository>();

builder.Services.AddTransient<DebtRepository>();
builder.Services.AddTransient<IncomeRepository>();
builder.Services.AddTransient<SavingRepository>();
builder.Services.AddTransient<SpendingRepository>();
builder.Services.AddTransient<StrategyRepository>();
builder.Services.AddTransient<InvestmentRepository>();

builder.Services.AddTransient<SavingsLongevityCalculationService>();

var app = builder.Build();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication()
   .UseAuthorization();

app.UseFastEndpoints(config =>
{
    config.Security.NameClaimType = ClaimTypes.Email;
});
app.UseSwaggerGen();

app.MapIdentityApi<User>();


app.Run();