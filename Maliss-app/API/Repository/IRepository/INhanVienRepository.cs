using API.Models;

namespace API.Repository.IRepository
{
    public interface INhanVienRepository : IRepository<NhanVien , string>
    {

        Task<IEnumerable<NhanVien>> GetAll();
    }
}
