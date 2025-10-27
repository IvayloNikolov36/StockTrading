using Prices.Service.DataServices;
using Prices.Service.ViewModels;

namespace Prices.Service.HostedServices
{
    public class StockPricesGeneratorService : BackgroundService
    {
        private const long IntervalInSeconds = 1;
        private const int MinValue = 50;
        private const int MaxValue = 50000;

        private const string QueueName = "stock_prices_q";
        private const string ExchangeName = "stock_prices";
        private const string RoutingKey = "stock_prices_rk";

        private readonly IServiceProvider serviceProvider;

        public StockPricesGeneratorService(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            IStocksService stocksService = scope.ServiceProvider
                .GetRequiredService<IStocksService>();

            IEventProducer eventProducer = scope.ServiceProvider
                .GetRequiredService<IEventProducer>();

            IEnumerable<StockViewModel> allStocks = await stocksService
                .GetAllStocks(stoppingToken);

            Random rnd = new();

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (StockViewModel stock in allStocks)
                {
                    stock.Price = this.GetPrice(rnd);
                }

                await eventProducer
                    .PublishEvent(QueueName, ExchangeName, RoutingKey, allStocks);

                // TODO: find another approach
                await Task.Delay(
                    TimeSpan.FromSeconds(IntervalInSeconds),
                    stoppingToken);
            }
        }

        private double GetPrice(Random random)
        {
            int value = random.Next(MinValue, MaxValue);
            double randomDouble = random.NextDouble();
            double multiplier = randomDouble == 0 ? 1 : randomDouble;

            return Math.Round(value * multiplier, 2);
        }
    }
}
