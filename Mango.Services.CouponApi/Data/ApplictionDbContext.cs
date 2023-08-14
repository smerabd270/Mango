using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Data
{
    public class ApplictionDbContext:DbContext
    {
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId=1,
                    CouponCode="180FF",
                    DissCountAmount=10,
                    MinAmount=20
                },
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "280FF",
                    DissCountAmount = 10,
                    MinAmount = 20
                }
                );
        }

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) :base(options) { }
        public DbSet<Coupon> coupons { get; set; }  
    }
   
   
}
