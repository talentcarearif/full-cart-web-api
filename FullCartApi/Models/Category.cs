using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FullCartApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string CategoryName { get; set; }
    }
}
