using API.Models.DTO;

namespace JollyWeb.Service.IService
{
    public interface INhanVienService : IApiService
    {
        Task<List<NhanVienDTO>> GetAll();
    }
}
