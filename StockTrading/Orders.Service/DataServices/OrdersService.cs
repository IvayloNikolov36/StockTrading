using Orders.Service.Data;
using Orders.Service.DataServices.Contracts;
using Orders.Service.Entities;
using Orders.Service.Entities.Enums;
using Orders.Service.Infrastructure.Exceptions;
using Orders.Service.Messaging.EventConsumer;
using Orders.Service.Messaging.EventProducer;
using Orders.Service.ViewModels;
using System.Text.Json;

namespace Orders.Service.DataServices;

public class OrdersService : IOrdersService
{
    // TODO: move to config file
    private const string QueueName = "stock_prices_q";
    private const string ExchangeName = "stock_prices";
    private const string RoutingKey = "stock_prices_rk";

    private const string OrderEventQueueName = "stock_order_q";
    private const string OrderEventExchangeName = "stock_order";
    private const string OrderEventRoutingKey = "stock_order_rk";

    private readonly OrdersDbContext dbContext;
    private readonly IEventConsumer eventConsumer;
    private readonly IEventProducer eventProducer;

    public OrdersService(
        OrdersDbContext dbContext,
        IEventConsumer eventConsumer,
        IEventProducer eventProducer)
    {
        this.dbContext = dbContext;
        this.eventConsumer = eventConsumer;
        this.eventProducer = eventProducer;
    }

    public async Task<OrderCreateResultViewModel> Create(string userId, OrderCreateViewModel order)
    {
        // TODO: add validation whether the user is able to sell

        string message = await this.eventConsumer.ConsumeMessage(QueueName, ExchangeName, RoutingKey);

        IEnumerable<StockViewModel> stocks = JsonSerializer
            .Deserialize<IEnumerable<StockViewModel>>(message)!;

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

        await this.eventProducer.PublishEvent(
            OrderEventQueueName,
            OrderEventExchangeName,
            OrderEventRoutingKey,
            orderEntity
        );

        return new OrderCreateResultViewModel
        {
            Price = orderStock.Price,
            Quantity = order.Quantity,
            Side = order.Side,
            Ticker = order.Ticker
        };
    }
}
