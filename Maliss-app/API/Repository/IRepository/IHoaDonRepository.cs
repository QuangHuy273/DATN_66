using API.Models;

namespace API.Repository.IRepository
{
    public interface IHoaDonRepository : IRepository<HoaDon , string>
    {
        Task UpdateOrderStatus(Guid hoaDonChiTietId, string newStatus, string reason);

        Task<List<LichSuTrangThai>> GetLichSu(string hoaDonId);
    }
}
