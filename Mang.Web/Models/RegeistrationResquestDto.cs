using System.ComponentModel.DataAnnotations;

namespace Mang.Web.Models
{
    public class RegeistrationResquestDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? RoleName { get; set; }   

    }
}
