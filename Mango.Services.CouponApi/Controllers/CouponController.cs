using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplictionDbContext _dbContext;
      
        public CouponController(ApplictionDbContext dbContext)
        {
            _dbContext= dbContext;  
        }
        [HttpGet]
        public object GetAll()
        {
            try
            {
                IEnumerable<Coupon>copounList=_dbContext.coupons.ToList();  
                return copounList;

            }
            catch (Exception ex) 
            {

            }
            return null;
        }
    }
}
