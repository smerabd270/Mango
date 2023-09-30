using Mang.Web.Models;
using Mang.Web.Services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mang.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> productIndex()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetAllProductAsync();
            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> productCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> productCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductAsync(model);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(productIndex));
                }
            }
            return View(model);

        }
        public async Task<IActionResult> productDelete(int productId)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.GetProductByIdAsync(productId);
                if (response != null && response.IsSuccess == true)
                {
                    ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> productDelete(ProductDto productDto)
        {
            if (productDto.ProductId != 0)
            {
                ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);
                if (response != null && response.IsSuccess == true)
                {
                    ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return RedirectToAction(nameof(productIndex));
                }
            }
            return View(productDto);

        }

    }
}

