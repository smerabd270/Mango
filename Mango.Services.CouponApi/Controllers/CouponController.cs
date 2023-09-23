using AutoMapper;
using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
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
        [Route("GetById/{id:int}")]
        public ResponseDto GetById(int id)

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
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)

        {
            try
            {
                Coupon obj = _dbContext.coupons.First(x => x.CouponCode == code);
                if(obj is null )
                {
                    _responseDto.IsSuccess=false;
                }
                _responseDto.Result = _mapper.Map<CouponDto>(obj);


            }
            
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)

        {
            try
            {
                Coupon obj =_mapper.Map<Coupon>(couponDto);
                _dbContext.coupons.Add(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "add Success";

            }

            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)

        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.coupons.Update(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "update Success";

            }

            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
            

        {
            try
            {
                Coupon obj = _dbContext.coupons.First(x => x.CouponId == id);
                _dbContext.coupons.Remove(obj);
                _dbContext.SaveChanges();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "delete Success";

            }

            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }


    }
}
