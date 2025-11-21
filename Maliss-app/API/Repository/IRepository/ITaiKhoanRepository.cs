using API.Models;

namespace API.Repository.IRepository
{
    public interface ITaiKhoanRepository : IRepository<TaiKhoan , Guid>
    {
        Task<TaiKhoan> GetByTaiKhoan(string tk );
        Task<TaiKhoan> GetKhByTaiKhoan(string tk);
    }
}
