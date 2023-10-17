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

			private readonly ApplictionDbContext _db;
			private readonly ResponseDto _response;
			private readonly IMapper _mapper;

			public CartApiController(ApplictionDbContext dbContext, IMapper mapper)
			{
				_db = dbContext;
				_response = new ResponseDto();
				_mapper = mapper;
			}
        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.cartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.cartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //if header is not null
                    //check if details has same product
                    var cartDetailsFromDb = await _db.cartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //create cartdetails
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                      cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.cartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }
        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.cartDetails.First(
                    u => u.CartDetailsId == cartDetailsId);
                int totalCountOfCartItem= _db.cartDetails.Where(
                    u=>u.CartHeaderId==cartDetails.CartHeaderId).Count();
                _db.cartDetails.Remove(cartDetails);
                if(totalCountOfCartItem==1)
                {
                    var cartHeaderToRemove=_db.cartHeaders.First(
                        u=>u.CartHeaderId==cartDetails.CartHeaderId);
                    _db.cartHeaders.Remove(cartHeaderToRemove);
                }
              await  _db.SaveChangesAsync();
                _response.Result = true;

            }
            catch(Exception ex)
            {
                _response.Message=ex.Message.ToString();
                _response.IsSuccess=false;
            }
            return _response;
        }
        [HttpGet("GetCart/{userId}")]
        public async Task <ResponseDto>GetCart(string userId)
        {
            try
            {
                CartDto cart = new CartDto()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_db.cartHeaders.First(
                        u => u.UserId == userId))

                };
                cart.CartDetails = _mapper.Map <IEnumerable<CartDetailsDto>>(_db.cartDetails.Where(
                    u => u.CartHeaderId == cart.CartHeader.CartHeaderId));
                foreach (var item in cart.CartDetails)
                {
                    cart.CartHeader.CartTotal+=(item.Count*Convert.ToDecimal( item.Product.Price));
                }
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }

    }
}
