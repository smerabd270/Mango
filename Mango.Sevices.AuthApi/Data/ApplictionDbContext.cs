using Microsoft.EntityFrameworkCore;

namespace Mango.Sevices.AuthApi.Data
{
    public class ApplictionDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options) { }
    }


}

