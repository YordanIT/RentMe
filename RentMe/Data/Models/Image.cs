using RentMe.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; init; }

        [MaxLength(Const.ImageMaxValue)]
        public byte[]? Data { get; set; }

        public bool IsDeleted { get; set; }
    }
}
