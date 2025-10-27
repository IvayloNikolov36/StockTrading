using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Data;
using Portfolio.Service.Entities;
using Portfolio.Service.Messaging.EventConsumer;
using System.Text.Json;

namespace Portfolio.Service.HostedServices
{
    public class StockOrderDbTableSynchronizeService : BackgroundService
    {
        private const string OrderEventQueueName = "stock_order_q";
        private const string OrderEventExchangeName = "stock_order";
        private const string OrderEventRoutingKey = "stock_order_rk";

        private readonly IServiceProvider serviceProvider;
        private readonly Queue<string> queue;

        public StockOrderDbTableSynchronizeService(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.queue = [];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = serviceProvider.CreateScope();

                PortfoliosDbContext dbContext = scope.ServiceProvider
                    .GetRequiredService<PortfoliosDbContext>();

                IEventConsumer eventConsumer = scope.ServiceProvider
                    .GetRequiredService<IEventConsumer>();

                eventConsumer.OnMessageReceived += EventConsumer_onMessageReceived;

                await eventConsumer.ConsumeMessage(
                        OrderEventQueueName,
                        OrderEventExchangeName,
                        OrderEventRoutingKey,
                        stoppingToken
                );

                if (queue.Count > 0)
                {
                    string msg = this.queue.Dequeue();
                    OrderEntity order = JsonSerializer.Deserialize<OrderEntity>(msg)!;
                    await this.WriteData(dbContext, stoppingToken, order);
                }
            }
        }

        private void EventConsumer_onMessageReceived(
            EventConsumer eventConsumer,
            MessageReceivedEventArgs eventArgs)
        {
            string message = eventArgs.Message;
            queue.Enqueue(message);
        }

        // TODO: create service and move this method
        private async Task WriteData(PortfoliosDbContext dbContext, CancellationToken stoppingToken, OrderEntity order)
        {
            bool userExists = await dbContext.Users.AnyAsync(u => u.Id == order.UserId);

            if (!userExists)
            {
                await dbContext.Users.AddAsync(new UserEntity { Id = order.UserId });
            }

            await dbContext.Orders.AddAsync(order, stoppingToken);
            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
