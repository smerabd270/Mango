using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace Mang.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;

        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View( await LoadCartDtoBaseOnLoggedInUser());
        }
        private async Task<CartDto> LoadCartDtoBaseOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);
            if(response!=null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
                return new CartDto();
        }
    }
}
