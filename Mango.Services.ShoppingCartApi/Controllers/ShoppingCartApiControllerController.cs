using AutoMapper;
using Mango.Services.ShoppingCartApi.Data;
using Mango.Services.ShoppingCartApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartApiControllerController : ControllerBase
    {
        private readonly ApplictionDbContext _dbContext;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;

        public ShoppingCartApiControllerController(ApplictionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }
        [HttpPost("CartUpser")]
        public async Task<ResponseDto> CartUpser(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _dbContext.cartHeaders.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
            }
            catch (Exception ex) 
            {
                _responseDto.Message= ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return null;
        }

    }
}
