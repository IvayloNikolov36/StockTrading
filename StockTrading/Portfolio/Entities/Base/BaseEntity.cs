
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Service.Entities.Base;

public class BaseEntity<T> : IAudit
{
    [Key]
    public required T Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
