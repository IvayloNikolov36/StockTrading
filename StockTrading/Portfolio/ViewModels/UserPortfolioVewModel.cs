namespace Portfolio.Service.ViewModels;

public class UserPortfolioVewModel
{
    public required string UserId { get; set; }

    public decimal BoughtStocksPrice { get; set; }

    public decimal SoldStocksPrice { get; set; }

    //public string? MostOrderedStock { get; set; }

    public IEnumerable<StockBasicViewModel> StockDetails { get; set; }
}
