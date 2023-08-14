using AutoMapper;
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
        private readonly IMapper _mapper;

        public CouponController(ApplictionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.coupons.ToList();
                _responseDto.Result= _mapper.Map<IEnumerable<CouponDto>>(objList);

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
                Coupon obj = _dbContext.coupons.First(x=>x.CouponId==id);
                _responseDto.Result= _mapper.Map<CouponDto>(obj);

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
