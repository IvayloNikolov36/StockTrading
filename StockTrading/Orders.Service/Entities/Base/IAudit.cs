namespace Orders.Service.Entities.Base;

public interface IAudit
{
    DateTime CreatedOn { get; set; }

    DateTime? ModifiedOn { get; set; }
}
