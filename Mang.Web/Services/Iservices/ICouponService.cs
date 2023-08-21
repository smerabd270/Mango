using Mang.Web.Models;

namespace Mang.Web.Services.Iservices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetCouponAsync(string Code);
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);




    }
}
