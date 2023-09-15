using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Logout()
        {
            return View();
        }
    }
}
