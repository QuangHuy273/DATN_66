using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ChiTietMonAnRepository : Repository<ChiTietMonAn, Guid>, IChiTietMonAnRepository
    {
        private readonly DBAppContext _context;
        public ChiTietMonAnRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChiTietMonAn>> GetMonAnId(string id)
        {
            return await _context.chiTietMonAns
                .Where(x => x.MonAnId == id)
                .Include(c => c.MonAn)
                .Include(c => c.LoaiVi)
                .Include(c => c.NguyenLieu)
                .Include(c => c.KichCo)
                .Include(c => c.NhaCungCap)
                .Include(c => c.Anhs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ChiTietMonAn>> GetAll()
        {
            return await _context.chiTietMonAns
                .Include(c => c.MonAn)
                .Include(c => c.LoaiVi)
                .Include(c => c.NguyenLieu)
                .Include(c => c.KichCo)
                .Include(c => c.NhaCungCap)
                .Include(c => c.Anhs)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
