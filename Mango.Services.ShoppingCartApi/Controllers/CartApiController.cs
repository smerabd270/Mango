using AutoMapper;
using Mango.Services.ShoppingCartApi.Data;
using Mango.Services.ShoppingCartApi.Models;
using Mango.Services.ShoppingCartApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController : ControllerBase
    {

			private readonly ApplictionDbContext _dbContext;
			private readonly ResponseDto _responseDto;
			private readonly IMapper _mapper;

			public CartApiController(ApplictionDbContext dbContext, IMapper mapper)
			{
				_dbContext = dbContext;
				_responseDto = new ResponseDto();
				_mapper = mapper;
			}
		[HttpPost("CartUpsert")]
		public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
				var cartHeaderFromDb = await _dbContext.cartHeaders.FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
				if(cartHeaderFromDb == null)
                {
					CartHeader cartHeader=_mapper.Map<CartHeader>(cartDto);	//
					_dbContext.cartHeaders.Add(cartHeader);
				 await	_dbContext.SaveChangesAsync();
					cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
					_dbContext.cartDetails.Add(_mapper.Map<CartDetails>(cartDto));
					await _dbContext.SaveChangesAsync();


					
                }
				else
                {
					var cartDetailsFromDb = await _dbContext.cartDetails.FirstOrDefaultAsync(
						u => u.ProductId == cartDto.CartDetails.First().ProductId &&
						u.CartHeaderId==cartHeaderFromDb.CartHeaderId);
					if(cartDetailsFromDb == null)
                    {
						cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
						_dbContext.cartDetails.Add(_mapper.Map<CartDetails>(cartDto));
						await _dbContext.SaveChangesAsync();
					}
                    else 
					{
						cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
						cartDto.CartDetails.First().CartHeaderId += cartDetailsFromDb.CartHeaderId;
						cartDto.CartDetails.First().CartDetailsId += cartDetailsFromDb.CartDetailsId;
						_dbContext.cartDetails.Update(_mapper.Map<CartDetails>(cartDto));
						await _dbContext.SaveChangesAsync();

					}

				}
				_responseDto.Result = cartDto;

            }
			catch (Exception ex)
            {
				_responseDto.Message= ex.Message.ToString();
				_responseDto.IsSuccess=false;
	
            }
			return _responseDto;
        }
		}
}
