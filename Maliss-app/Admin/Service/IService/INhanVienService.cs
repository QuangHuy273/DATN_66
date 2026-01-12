using API.Models;
using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface INhanVienService : IApiService
    {
        Task<List<NhanVienDTO>> GetAll();
        Task<bool> Create(NhanVienDTO dto);
        Task<bool> ChangeStatus(string id);
        Task<bool> Update(NhanVienDTO dto);
        Task<bool> Delete(string id);

    }
}
