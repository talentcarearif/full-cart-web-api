using System.ComponentModel.DataAnnotations;

namespace FullCartApi.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string RoleName { get; set; }
    }
}
