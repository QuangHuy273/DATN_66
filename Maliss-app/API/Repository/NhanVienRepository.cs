using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class NhanVienRepository : Repository<NhanVien, string>, INhanVienRepository
    {
        private readonly DBAppContext _context;
        public NhanVienRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAll()
        {
            return await _context.nhanViens
            .Include(kh => kh.NguoiDung)
            .ThenInclude(nd => nd.DiaChis)
            .Include(nv => nv.ChucVu)
            .ToListAsync();
        }
    }
}
