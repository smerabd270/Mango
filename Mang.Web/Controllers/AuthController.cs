using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mang.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuhtService _auhtService;

        public AuthController(IAuhtService auhtService)
        {
            _auhtService = auhtService;
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
        public IActionResult Logout()
        {
            return View();
        }
    }
}
