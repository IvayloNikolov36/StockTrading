using Prices.Service.ViewModels;

namespace Prices.Service.DataServices;

public interface IStocksService
{
    Task<IEnumerable<StockViewModel>> GetAllStocks(CancellationToken cancellationToken);
}
