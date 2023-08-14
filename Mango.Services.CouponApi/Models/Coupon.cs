namespace Mango.Services.CouponApi.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = null!;
        public double DissCountAmount { get; set; }
        public int MinAmount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
