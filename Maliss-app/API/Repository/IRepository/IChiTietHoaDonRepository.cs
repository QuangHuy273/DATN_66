using API.Models;

namespace API.Repository.IRepository
{
    public interface IChiTietHoaDonRepository : IRepository<HoaDonChiTiet , Guid>
    {
        Task<IEnumerable<HoaDonChiTiet>> GetHoaDonId(string Id);
    }
}
