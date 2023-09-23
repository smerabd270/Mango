using Mango.Sevices.AuthApi.Models;
using System.Threading.Tasks;

namespace Mango.Sevices.AuthApi.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> roles);
    }
}
