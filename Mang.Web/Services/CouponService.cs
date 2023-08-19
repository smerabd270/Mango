using Mang.Services.CouponApi.Models.Dto;
using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Mang.Web.Utility;

namespace Mang.Web.Services
{
    public class CouponService : ICouponService
    {
        private IBaseService _baseService;
        public CouponService( IBaseService baseService)
        {
            _baseService = baseService; 
        }
        public async  Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data= couponDto,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async  Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url =SD.CouponApiBase+"/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string Code)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/GetByCode/"+Code
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async  Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendASync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }
    }
}
