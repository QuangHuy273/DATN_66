using API.Models;
using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IKhachHangService : IApiService
    {
        Task<List<KhachHangDTO>> GetAll();
        Task<bool> Create(KhachHangDTO dto);
        Task<bool> ChangeStatus(string id);
        Task<bool> Update(KhachHangDTO dto);
        Task<bool> Delete(string id);
    }
}
