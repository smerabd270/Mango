using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mang.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            ResponseDto? response = await  _couponService.GetAllCouponAsync();
            if(response != null && response.IsSuccess== true )
            {
                list= JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
    }
}
