
using System.ComponentModel.DataAnnotations;

namespace Orders.Service.Entities.Base;

public class BaseEntity<T> : IAudit
{
    [Key]
    public T Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
