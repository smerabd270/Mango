namespace Mango.Services.CouponApi.Models.Dto
{
    public class CouponDto
    {
        public string CouponCode { get; set; } = null!;
        public double DissCountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
