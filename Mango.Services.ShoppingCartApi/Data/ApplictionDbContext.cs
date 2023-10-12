using Mango.Services.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.Data
{
    public class ApplictionDbContext:DbContext
    {
       
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) :base(options) { }
        public DbSet<CartHeader> cartHeaders { get; set; }

        public DbSet<CartDetails> cartDetails { get; set; }
    }


}
