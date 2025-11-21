using API.Models;

namespace API.Repository.IRepository
{
    public interface IKhachHangRepository : IRepository<KhachHang , string>
    {
        Task<IEnumerable<KhachHang>> GetAll();
    }
}
