using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullCartApi.Models
{
    public class UserMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Mobile { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(20)]
        public required string UserType { get; set; } = "Active";

        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        [ValidateNever]
        public UserRole? UserRole { get; set; }

        [Required]
        public required DateTime CreateDate { get; set; }
    }
}
