using Mango.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Data
{
    public class ApplictionDbContext:DbContext
    {
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    //        modelBuilder.Entity<Product>().HasData(
    //            new Product
				//{
    //                ProductId=1,
    //                CouponCode="180FF",
    //                DissCountAmount=10,
    //                MinAmount=20
    //            },
    //            new Product
				//{
    //                CouponId = 2,
    //                CouponCode = "280FF",
    //                DissCountAmount = 10,
    //                MinAmount = 20
    //            }
    //            );
        }

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) :base(options) { }
        public DbSet<Product> products { get; set; }  
    }
   
   
}
