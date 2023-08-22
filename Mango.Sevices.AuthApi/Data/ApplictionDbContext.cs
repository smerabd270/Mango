using Mango.Sevices.AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Sevices.AuthApi.Data
{
    public class ApplictionDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
    }


}

