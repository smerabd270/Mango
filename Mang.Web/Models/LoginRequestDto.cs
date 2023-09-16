using System.ComponentModel.DataAnnotations;

namespace Mang.Web.Models 
{
    public class LoginRequestDto
    {
       public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
