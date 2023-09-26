using AutoMapper;
using Mango.Services.ProductApi.Data;
using Mango.Services.ProductApi.Models;
using Mango.Services.ProductApi.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductApi.Controllers
{
	[Route("api/product")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProductApi : ControllerBase
	{
		private readonly ApplictionDbContext _dbContext;
		private readonly ResponseDto _responseDto;
		private readonly IMapper _mapper;

		public ProductApi(ApplictionDbContext dbContext, IMapper mapper)
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
				IEnumerable<Product> objList = _dbContext.Products.ToList();

				_responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);

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
				Product obj = _dbContext.Products.First(x => x.ProductId == id);
				_responseDto.Result = _mapper.Map<ProductDto>(obj);

			}
			catch (Exception ex)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = ex.Message;

			}
			return _responseDto;

		}
		[HttpGet]
		[Route("GetByName/{name}")]
		public ResponseDto GetByName(string name)

		{
			try
			{
				Product obj = _dbContext.Products.First(x => x.Name == name);
				if (obj is null)
				{
					_responseDto.IsSuccess = false;
				}
				_responseDto.Result = _mapper.Map<ProductDto>(obj);


			}

			catch (Exception ex)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = ex.Message;

			}
			return _responseDto;

		}
		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Post([FromBody] ProductDto productDto)

		{
			try
			{
				Product obj = _mapper.Map<Product>(productDto);
				_dbContext.Products.Add(obj);
				_dbContext.SaveChanges();
				_responseDto.Result = _mapper.Map<ProductDto>(obj);
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
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Put([FromBody] ProductDto couponDto)

		{
			try
			{
				Product obj = _mapper.Map<Product>(couponDto);
				_dbContext.Products.Update(obj);
				_dbContext.SaveChanges();
				_responseDto.Result = _mapper.Map<ProductDto>(obj);
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
		[Authorize(Roles = "ADMIN")]
		[Route("{id:int}")]
		public ResponseDto Delete(int id)


		{
			try
			{
				Product obj = _dbContext.Products.First(x => x.ProductId == id);
				_dbContext.Products.Remove(obj);
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
