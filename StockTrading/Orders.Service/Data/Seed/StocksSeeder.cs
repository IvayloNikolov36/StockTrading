using Microsoft.EntityFrameworkCore;
using Orders.Service.Entities;

namespace Orders.Service.Data.Seed;

public class StocksSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockEntity>().HasData(
            new StockEntity
            {
                Id = 1,
                Ticker = "NVDA",
                CompanyName = "NVIDIA Corporation"
            },
            new StockEntity
            {
                Id = 2,
                Ticker = "AAPL",
                CompanyName = "Apple Inc."
            },
            new StockEntity
            {
                Id = 3,
                Ticker = "MSFT",
                CompanyName = "Microsoft Corporation"
            },
            new StockEntity
            {
                Id = 4,
                Ticker = "GOOGL",
                CompanyName = "Alphabet Inc."
            },
            new StockEntity
            {
                Id = 5,
                Ticker = "AMZN",
                CompanyName = "Amazon.com, Inc."
            },
            new StockEntity
            {
                Id = 6,
                Ticker = "META",
                CompanyName = "Meta Platforms, Inc."
            },
            new StockEntity
            {
                Id = 7,
                Ticker = "AVGO",
                CompanyName = "Broadcom Inc."
            },
            new StockEntity
            {
                Id = 8,
                Ticker = "TSLA",
                CompanyName = "Tesla, Inc."
            },
            new StockEntity
            {
                Id = 9,
                Ticker = "ORCL",
                CompanyName = "Oracle Corporation"
            },
            new StockEntity
            {
                Id = 10,
                Ticker = "LLY",
                CompanyName = "Eli Lilly and Company"
            }
        );
    }
}
