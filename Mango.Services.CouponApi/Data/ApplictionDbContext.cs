using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Data
{
    public class ApplictionDbContext:DbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) :base(options) { }
        public DbSet<Coupon> coupons { get; set; }  
    }
}
