namespace Orders.Service.ViewModels;

public class StockViewModel
{
    public int Id { get; set; }

    public string Ticker { get; set; } = string.Empty;

    public double Price { get; set; }
}
