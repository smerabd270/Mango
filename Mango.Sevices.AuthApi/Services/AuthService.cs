using Mango.Sevices.AuthApi.Data;
using Mango.Sevices.AuthApi.Models;
using Mango.Sevices.AuthApi.Models.Dto;
using Mango.Sevices.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mango.Sevices.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplictionDbContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;
        public AuthService(ApplictionDbContext db, UserManager<ApplicationUser> userManger,
                            RoleManager<IdentityRole> roleManger, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManger = userManger;
            _roleManger = roleManger;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if(!_roleManger.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManger.CreateAsync( new IdentityRole( roleName)).GetAwaiter().GetResult();
                }
                await _userManger.AddToRoleAsync(user, roleName);   
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.Where(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()).FirstOrDefault();
            bool isValid = await _userManger.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            //if user found 
            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                Name = user.Name
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Rejester(RegeistrationResquestDto regeistrationResquestDto)
        {
            ApplicationUser user = new()
            {
                Name = regeistrationResquestDto.Name,
                UserName = regeistrationResquestDto.Email,
                Email = regeistrationResquestDto.Email,
                PhoneNumber = regeistrationResquestDto.PhoneNumber,
                NormalizedUserName = regeistrationResquestDto.Email.ToUpper()
            };
            try
            {
                var result = await _userManger.CreateAsync(user, regeistrationResquestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(x => x.UserName == regeistrationResquestDto.Email);
                    UserDto userDto = new()
                    {
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        PhoneNumber = userToReturn.PhoneNumber

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }



            catch (Exception ex)
            {

            }
            return "Error EnCountered";
        }
    }
}
