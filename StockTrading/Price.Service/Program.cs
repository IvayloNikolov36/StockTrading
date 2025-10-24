using Microsoft.EntityFrameworkCore;
using Prices.Service;
using Prices.Service.Data;
using Prices.Service.DataServices;
using Prices.Service.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StocksDbContext>(options =>
{
    string? connectionString = builder.Configuration
        .GetConnectionString("DefaultConnection");

    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IEventProducer, EventProducer>();

builder.Services
    .AddHostedService<StockPricesGeneratorService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        using StocksDbContext? context = scope.ServiceProvider
            .GetService<StocksDbContext>();

        context?.Database.Migrate();
    }
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
