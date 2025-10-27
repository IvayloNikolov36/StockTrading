using Portfolio.Service.Entities.Base;
using Portfolio.Service.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Service.Entities
{
    public class OrderEntity : BaseEntity<string>
    {
        public OrderEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public required string UserId { get; set; }
        public UserEntity User { get; set; }

        public int StockId { get; set; }
        public StockEntity Stock { get; set; }

        [MinLength(1)]
        public int Quantity { get; set; }

        // TODO: use decimal for price everywhere
        public decimal Price { get; set; }

        public SideEnum Side { get; set; }
    }
}
