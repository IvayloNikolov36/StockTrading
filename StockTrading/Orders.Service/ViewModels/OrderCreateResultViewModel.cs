namespace Orders.Service.ViewModels;

public class OrderCreateResultViewModel
{
    public required string Ticker { get; set; }

    public required int Quantity { get; set; }

    public required string Side { get; set; }

    public required double Price { get; set; }
}
