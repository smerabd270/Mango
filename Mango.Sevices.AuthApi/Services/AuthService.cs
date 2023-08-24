﻿using Mango.Sevices.AuthApi.Data;
using Mango.Sevices.AuthApi.Models;
using Mango.Sevices.AuthApi.Models.Dto;
using Mango.Sevices.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mango.Sevices.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplictionDbContext _db;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;
        public AuthService(ApplictionDbContext db, UserManager<ApplicationUser> userManger,
                            RoleManager<IdentityRole> roleManger)
        {
            _db = db;
            _userManger = userManger;
            _roleManger = roleManger;

        }
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            
            throw new NotImplementedException();
        }

        public  async Task<string> Rejester(RegeistrationResquestDto regeistrationResquestDto)
        {
            ApplicationUser user = new()
            {
                Name= regeistrationResquestDto.Name,
                UserName=regeistrationResquestDto.Email,
                Email=regeistrationResquestDto.Email,
                PhoneNumber=regeistrationResquestDto.PhoneNumber,
                NormalizedUserName=regeistrationResquestDto.Email.ToUpper()
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
