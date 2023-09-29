﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductApi.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		[Required]
		[StringLength(100)]
		public string Name { get; set; }
		[Range(1,1000)]
		public double Price { get; set; }
		public string Description { get; set; }

		public string CategoryName { get; set; }
		public string ImageUrl { get; set; }
	}
}