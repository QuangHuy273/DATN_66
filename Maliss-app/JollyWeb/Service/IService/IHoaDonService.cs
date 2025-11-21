using API.Models;

namespace JollyWeb.Service.IService
{
    public interface IHoaDonService
    {
        Task<bool> LichSuTrangThai(Guid orderId, string newStatus, string reason);
        Task<List<LichSuTrangThai>> GetLichSuTrangThaiByHoaDon(string hoaDonId);
    }
}
