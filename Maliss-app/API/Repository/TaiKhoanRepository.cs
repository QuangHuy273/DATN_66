using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class TaiKhoanRepository : Repository<TaiKhoan, Guid>, ITaiKhoanRepository
    {
        private readonly DBAppContext _context;
        public TaiKhoanRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TaiKhoan> GetByTaiKhoan(string tk )
        {
            var check = await _context.taiKhoans
            .Include(t => t.NguoiDung)
                .ThenInclude(nd => nd.NhanVien)
                    .ThenInclude(nv => nv.ChucVu)
            .FirstOrDefaultAsync(t => t.UserName == tk );
            if (check == null)
            {
                return null;
            }
            return check;
        }

        public async Task<TaiKhoan> GetKhByTaiKhoan(string tk)
        {
            var check = await _context.taiKhoans
            .Include(t => t.NguoiDung)
                .ThenInclude(nd => nd.KhachHang)
            .FirstOrDefaultAsync(t => t.UserName == tk);
            if (check == null)
            {
                return null;
            }
            return check;
        }
    }
}
