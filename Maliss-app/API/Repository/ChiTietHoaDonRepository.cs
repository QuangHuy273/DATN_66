using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ChiTietHoaDonRepository : Repository<HoaDonChiTiet, Guid>, IChiTietHoaDonRepository
    {
        private readonly DBAppContext _context;
        public ChiTietHoaDonRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HoaDonChiTiet>> GetHoaDonId(string Id)
        {
            return await _context.hoaDonChiTiets
                .Where(hdct => hdct.HoaDonId == Id)
                .Include(c => c.Combo)
                .Include(ctma => ctma.ChiTietMonAn)
                .Include(hd => hd.HoaDon)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
