using Orders.Service.Data;
using Orders.Service.DataServices.Contracts;
using Orders.Service.Entities;
using Orders.Service.Entities.Enums;
using Orders.Service.Infrastructure.Exceptions;
using Orders.Service.ViewModels;
using System.Text.Json;

namespace Orders.Service.DataServices;

public class OrdersService : IOrdersService
{
    // TODO: move to config file
    private const string QueueName = "stock_prices_q";
    private const string ExchangeName = "stock_prices";
    private const string RoutingKey = "stock_prices_rk";

    private readonly OrdersDbContext dbContext;
    private readonly IEventConsumer eventConsumer;

    public OrdersService(
        OrdersDbContext dbContext,
        IEventConsumer eventConsumer)
    {
        this.dbContext = dbContext;
        this.eventConsumer = eventConsumer;
    }

    public async Task<OrderCreateResultViewModel> Create(string userId, OrderCreateViewModel order)
    {
        string message = await this.eventConsumer
            .ConsumeMessage(QueueName, ExchangeName, RoutingKey);
        
        IEnumerable<StockViewModel>? stocks = JsonSerializer
            .Deserialize<IEnumerable<StockViewModel>>(message)
            ?? throw new ActionableException("Cannot create the order!");

        StockViewModel orderStock = stocks
            .SingleOrDefault(s => s.Ticker == order.Ticker!.ToUpper())
            ?? throw new ActionableException("Not existing stock with given ticker!");

        OrderEntity orderEntity = new()
        {
            UserId = userId,
            Quantity = order.Quantity,
            TickerId = orderStock.Id,
            Price = orderStock.Price,
            Side = (SideEnum)Enum.Parse(typeof(SideEnum), order.Side!)
        };

        await this.dbContext.AddAsync(orderEntity);

        await this.dbContext.SaveChangesAsync();

        return new OrderCreateResultViewModel
        {
            Price = orderStock.Price,
            Quantity = order.Quantity,
            Side = order.Side,
            Ticker = order.Ticker
        };
    }
}
