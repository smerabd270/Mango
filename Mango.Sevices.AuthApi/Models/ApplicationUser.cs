using Microsoft.AspNetCore.Identity;

namespace Mango.Sevices.AuthApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
