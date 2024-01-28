using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullCartApi.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
        public int Count { get; set; }

        [Required]
        public int UserMasterId { get; set; }

        [ForeignKey("UserMasterId")]
        [ValidateNever]
        public UserMaster? UserMaster { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
