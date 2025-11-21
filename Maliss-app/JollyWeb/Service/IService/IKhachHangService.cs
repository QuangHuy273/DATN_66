using API.Models;
using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface IKhachHangService : IApiService
    {
        Task<List<KhachHangDTO>> GetAll();
    }
}
