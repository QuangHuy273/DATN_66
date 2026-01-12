using API.Models;
using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IChiTietMonAnService : IApiService
    {
        Task<List<ChiTietMonAnDTO>> GetChiTiet(string Id);
        Task<List<ChiTietMonAnDTO>> GetAll();
        Task<ChiTietMonAnDTO?> GetById(Guid id);
        Task<ChiTietMonAnDTO?> Create(ChiTietMonAnDTO chiTietMonAn);
        Task<bool> Update(ChiTietMonAnDTO chiTietMonAn);
        Task<bool> Delete(Guid id);
    }
}
