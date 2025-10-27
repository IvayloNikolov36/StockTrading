using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Data;
using Portfolio.Service.DataServices;
using Portfolio.Service.HostedServices;
using Portfolio.Service.Messaging.EventConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserPortfolioService, UserPortfolioService>();

builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services
    .AddHostedService<StockOrderDbTableSynchronizeService>();

builder.Services
    .AddDbContext<PortfoliosDbContext>(options =>
    {
        string? connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection");

        options.UseNpgsql(connectionString);
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        using PortfoliosDbContext? context = scope.ServiceProvider
            .GetService<PortfoliosDbContext>();

        context?.Database.Migrate();
    }

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
