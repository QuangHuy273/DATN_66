using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface INhanVienService : IApiService
    {
        Task<List<NhanVienDTO>> GetAll();
    }
}
