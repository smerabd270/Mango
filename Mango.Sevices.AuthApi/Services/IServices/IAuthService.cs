using Mango.Sevices.AuthApi.Models.Dto;

namespace Mango.Sevices.AuthApi.Services.IServices
{
    public interface IAuthService
    {
        Task<string>Rejester(RegeistrationResquestDto regeistrationResquestDto);
        Task<LoginResponseDto>Login(LoginRequestDto loginRequestDto);

    }
}
