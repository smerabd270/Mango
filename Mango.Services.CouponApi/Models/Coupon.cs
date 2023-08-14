using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponApi.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; } = null!;
        [Required]
        public double DissCountAmount { get; set; }
        public int MinAmount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
