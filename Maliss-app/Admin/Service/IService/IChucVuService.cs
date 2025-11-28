using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IChucVuService : IApiService
    {
        Task<List<ChucVuDTO>> GetAll();
    }
}
