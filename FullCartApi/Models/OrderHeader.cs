using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullCartApi.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserMasterId { get; set; }

        [ForeignKey("UserMasterId")]
        [ValidateNever]
        public UserMaster? UserMaster { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        [MaxLength(20)]
        public required string OrderStatus { get; set; }

        [MaxLength(20)]
        public string? PaymentStatus { get; set; }

        [MaxLength(100)]
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }

        [MaxLength(100)]
        public string? SessionId { get; set; }
    }
}
