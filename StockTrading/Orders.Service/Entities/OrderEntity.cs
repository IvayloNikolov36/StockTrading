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
    public string UserId { get; set; }

    public int TickerId { get; set; }
    public StockEntity? Ticker { get; set; }

    [Required]
    [MinLength(1)]
    public int Quantity { get; set; }

    [Required]
    public SideEnum Side { get; set; }
}
