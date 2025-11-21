using API.Models;
using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IKhachHangService : IApiService
    {
        Task<List<KhachHangDTO>> GetAll();
    }
}
