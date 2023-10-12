using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartApi.Models.Dto
{
    public class CartHeaderDto
    {
       
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }
        public decimal CartTotal { get; set; }
    }
}
