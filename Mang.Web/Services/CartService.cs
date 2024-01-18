using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;

namespace Mang.Web.Services
{
    public class CartService : ICartService
    {
        private IBaseService _baseService;
        public CartService( IBaseService baseService)
        {
            _baseService = baseService; 
        }
        public async  Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data= cartDto,
                Url = SD.ShoppingCartApiBase + "/api/cart/ApplyCoupon"
            });
        }
        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartApiBase + "/api/cart/GetCart/"+userId
            });
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data=cartDetailsId,
                Url = SD.ShoppingCartApiBase + "/api/cart/RemoveCart"
            });
        }
        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartApiBase + "/api/cart/CartUpsert"
            });
        }

    }
}
