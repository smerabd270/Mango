using Mango.Sevices.AuthApi.Models;
using Mango.Sevices.AuthApi.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Sevices.AuthApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser applicationUser,IEnumerable<string>roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var climList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName)
            };
            climList.AddRange(roles.Select( role=>new Claim(ClaimTypes.Role, role)));
            var tokenDesceptor = new SecurityTokenDescriptor
            { Audience=_jwtOptions.Aduience,
               Issuer= _jwtOptions.Issuer,
               Subject=new ClaimsIdentity(climList),
               Expires=DateTime.Now.AddDays(7),
               SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token =tokenHandler.CreateToken(tokenDesceptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
