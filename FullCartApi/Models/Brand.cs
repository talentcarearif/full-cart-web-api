using System.ComponentModel.DataAnnotations;

namespace FullCartApi.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string BrandName { get; set; }
    }
}
