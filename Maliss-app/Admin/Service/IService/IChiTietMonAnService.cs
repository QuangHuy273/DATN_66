using API.Models;
using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IChiTietMonAnService : IApiService
    {
        Task<List<ChiTietMonAnDTO>> GetChiTiet(string Id);
        Task<List<ChiTietMonAnDTO>> GetAll();

    }
}
