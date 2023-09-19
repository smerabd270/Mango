using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mang.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuhtService _auhtService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuhtService auhtService, ITokenProvider tokenProvider)
        {
            _auhtService = auhtService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _auhtService.LoginAsync(obj);
            if(responseDto!= null&& responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError",responseDto.Message);
                return View(obj);

            }
        }
      
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text= SD.RoleADMIN,Value=SD.RoleADMIN },
                new SelectListItem{Text= SD.RoleCustomer,Value=SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;


            return View();
        }
         [HttpPost]
        public  async Task< IActionResult> Register( RegeistrationResquestDto obj)
        {
            ResponseDto result= await _auhtService.RegisterAsync(obj);
            ResponseDto assignRole;
            if (result!= null&& result.IsSuccess)
            {
                if(string.IsNullOrEmpty(obj.RoleName))
                {
                    obj.RoleName = SD.RoleCustomer;
                }
                assignRole = await _auhtService.AssignRoleAsyncAsync(obj);
                if(assignRole!=null&&assignRole.IsSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text= SD.RoleADMIN,Value=SD.RoleADMIN },
                new SelectListItem{Text= SD.RoleCustomer,Value=SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;


            return View(obj);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt=handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim( new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        }
    }
}
