using Orders.Service.Entities.Base;
using Orders.Service.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.Service.Entities;

public class OrderEntity : BaseEntity<string>
{
    public OrderEntity()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    public required string UserId { get; set; }

    public int TickerId { get; set; }
    public StockEntity? Ticker { get; set; }

    [MinLength(1)]
    public int Quantity { get; set; }

    public double Price { get; set; }

    public SideEnum Side { get; set; }
}
