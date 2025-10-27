using Microsoft.EntityFrameworkCore;
using Orders.Service.Data;
using Orders.Service.DataServices;
using Orders.Service.DataServices.Contracts;
using Orders.Service.Infrastructure.Middlewares;
using Orders.Service.Messaging.EventConsumer;
using Orders.Service.Messaging.EventProducer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddScoped<IEventProducer, EventProducer>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        using OrdersDbContext? context = scope.ServiceProvider
            .GetService<OrdersDbContext>();

        context?.Database.Migrate();
    }

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
