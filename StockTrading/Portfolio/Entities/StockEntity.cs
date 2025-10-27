using Portfolio.Service.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Service.Entities
{
    public class StockEntity : BaseEntity<int>
    {
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public required string Ticker { get; set; }

        [Required]
        [StringLength(90, MinimumLength = 2)]
        public required string CompanyName { get; set; }
    }
}
