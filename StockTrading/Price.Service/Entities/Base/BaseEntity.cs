using System.ComponentModel.DataAnnotations;

namespace Prices.Service.Entities.Base;

public class BaseEntity<T> : IAudit
{
    [Key]
    public T Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
