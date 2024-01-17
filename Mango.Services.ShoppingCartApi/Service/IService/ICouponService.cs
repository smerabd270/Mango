using Mango.Services.ShoppingCartApi.Models.Dto;

namespace Mango.Services.ShoppingCartApi.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string copuonCode);
    }
}
