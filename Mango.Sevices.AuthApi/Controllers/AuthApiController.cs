using Mango.Sevices.AuthApi.Models.Dto;
using Mango.Sevices.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Sevices.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responsce;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _responsce = new ();

        }
        [HttpPost("register")]
        public async Task<IActionResult>Register([FromBody] RegeistrationResquestDto model)
        {
            var errorMessage = await _authService.Rejester(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _responsce.IsSuccess = false;
                _responsce.Message = errorMessage;
                return BadRequest(_responsce);
            }
            return Ok(_responsce);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody] LoginRequestDto model)
        {
            var loginResponse =  await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _responsce.IsSuccess = false;
                _responsce.Message = "UserName or Password incorrect";
                return BadRequest(loginResponse);
            } _responsce.Result = loginResponse;
            return Ok(_responsce);
        }
    }
}
