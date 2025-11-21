using API.Models;

namespace Admin.Service.IService
{
    public interface IHoaDonService
    {
        Task<bool> LichSuTrangThai(Guid orderId, string newStatus, string reason);
        Task<List<LichSuTrangThai>> GetLichSu(string hoadonId); 
    }
}
