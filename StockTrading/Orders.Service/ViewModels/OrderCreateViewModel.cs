using Orders.Service.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.Service.ViewModels;

public class OrderCreateViewModel : IValidatableObject
{
    [Required]
    public string? Ticker { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public string? Side { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        string[] sideAllowedValues = Enum.GetNames(typeof(SideEnum));
        
        if (!sideAllowedValues.Contains(this.Side))
        {
            yield return new ValidationResult(
                $"Allowed values for 'Side' are {string.Join(", ", sideAllowedValues)}");
        }
    }
}
