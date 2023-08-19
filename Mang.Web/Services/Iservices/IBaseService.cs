using Mang.Web.Models;

namespace Mang.Web.Services.Iservices
{
    public interface IBaseService
    {
        Task<ResponseDto?>SendASync(RequestDto requestDto);
    }
}
