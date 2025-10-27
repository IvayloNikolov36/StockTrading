namespace Portfolio.Service.ViewModels
{
    public class StockBasicViewModel
    {
        public required string Ticker { get; set; }

        public required StockPriceBasicViewModel SoldDetails { get; set; }

        public required StockPriceBasicViewModel BoughtDetails { get; set; }
    }
}
