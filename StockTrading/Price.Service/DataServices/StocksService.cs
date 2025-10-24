using Microsoft.EntityFrameworkCore;
using Prices.Service.Data;
using Prices.Service.ViewModels;

namespace Prices.Service.DataServices;

public class StocksService : IStocksService
{
    private readonly StocksDbContext dbContext;

    public StocksService(StocksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<StockViewModel>> GetAllStocks(
        CancellationToken cancellationToken)
    {
        return await this.dbContext.Stocks.AsNoTracking()
            .Select(x => new StockViewModel
            {
                Id = x.Id,
                Ticker = x.Ticker
            })
            .ToListAsync(cancellationToken);
    }
}
