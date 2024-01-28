using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FullCartApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ProductName { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }       

        [Required]
        public double Price { get; set; }
       
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand? Brand { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped] 
        public int? CartCount { get; set;}
    }
}
