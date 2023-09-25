using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;

namespace Mang.Web.Services
{
    public class AuhtService : IAuhtService
    {
        private IBaseService _baseService;
        public AuhtService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignRoleAsyncAsync(RegeistrationResquestDto regeRegistrationDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = regeRegistrationDto,
                Url = SD.AuthApiBase + "/api/Auth/AssignRole"
            }, withBearer: false);
        }

        public  async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthApiBase + "/api/Auth/login"
            }
            ,withBearer:false);
            
        }

        public async Task<ResponseDto?> RegisterAsync(RegeistrationResquestDto regeRegistrationDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = regeRegistrationDto,
                Url = SD.AuthApiBase + "/api/Auth/register"
            },withBearer: false);
        }
    }
}
