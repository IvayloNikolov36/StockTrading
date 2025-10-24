using Prices.Service.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Prices.Service.Entities
{
    public class StockEntity : BaseEntity<int>
    {
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        [StringLength(90, MinimumLength = 2)]
        public string? CompanyName { get; set; }
    }
}
