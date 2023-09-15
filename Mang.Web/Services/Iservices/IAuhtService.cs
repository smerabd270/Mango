using Mang.Web.Models;

namespace Mang.Web.Services.Iservices
{
    public interface IAuhtService
    {
        Task<ResponseDto?>LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?>RegisterAsync(RegeistrationResquestDto regeRegistrationDto);
        Task<ResponseDto?> AssignRoleAsyncAsync(RegeistrationResquestDto regeRegistrationDto);

    }
}
