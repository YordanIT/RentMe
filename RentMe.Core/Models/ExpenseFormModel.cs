using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class ExpenseFormModel
    {
        public decimal Rent { get; set; }
        public decimal EntranceFee { get; set; }
        public decimal Heating { get; set; }
        public decimal Water { get; set; }
        public decimal Electricity { get; set; }
        public decimal? Other { get; set; }

        [StringLength(Const.CommentMaxLength, 
             ErrorMessage = "{0} must be between less than {1} symbols")]
        public string? Comment { get; set; }

        [Required]
        public string? TenantEmail { get; set; }
    }
}
