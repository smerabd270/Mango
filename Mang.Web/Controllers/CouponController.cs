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
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate( CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response= await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(CouponIndex));   
                }
            }
            return View(model);

        }
        public async Task<IActionResult> CouponDelete( int CouponId)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.GetCouponByIdAsync(CouponId);
                if (response != null && response.IsSuccess == true)
                {
                    CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            if (couponDto.CouponId!= 0)
            {
                ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
                if (response != null && response.IsSuccess == true)
                {
                    CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return View(couponDto);

        }

    }
}
