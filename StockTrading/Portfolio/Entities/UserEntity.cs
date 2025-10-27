using Portfolio.Service.Entities.Base;

namespace Portfolio.Service.Entities;

public class UserEntity : BaseEntity<string>
{
    public UserEntity()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Orders = [];
    }

    public ICollection<OrderEntity> Orders { get; set; }
}
