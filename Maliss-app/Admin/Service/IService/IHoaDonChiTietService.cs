using API.Models.DTO;

namespace Admin.Service.IService
{
    public interface IHoaDonChiTietService :IApiService
    {
        Task<List<HoaDonChiTietDTO>> GetChiTiet(string Id);
    }
}
