using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplictionDbContext _dbContext;
        private readonly ResponseDto _responseDto;

        public CouponController(ApplictionDbContext dbContext)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<Coupon> copounList = _dbContext.coupons.ToList();
                _responseDto.Result= copounList;

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)

        {
            try
            {
                Coupon copoun = _dbContext.coupons.First(x=>x.CouponId==id);
                _responseDto.Result= copoun;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess=false; 
                _responseDto.Message=ex.Message;

            }
            return _responseDto;

        }
    }
}
