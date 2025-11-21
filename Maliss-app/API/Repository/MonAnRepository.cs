using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class MonAnRepository : Repository<MonAn, string>, IMonAnRepository
    {
        private readonly DBAppContext _context;
        public MonAnRepository(DBAppContext context) : base(context)
        {
            _context = context;
            
        }

        public override async Task<IEnumerable<MonAn>> GetAllAsync()
        {
            return await _context.monAns
                .Include(m => m.ChiTietMonAns)?
                .ThenInclude(m => m.Anhs)
                .Include(m => m.TheLoai)
                .Include(m => m.ThuongHieu)
                .ToListAsync();
        }

        public override async Task<MonAn> GetByIdAsync(string id)
        {
              return await _context.monAns
                .Include(m => m.TheLoai)
                .Include(m => m.ThuongHieu)
                .Include(m => m.ChiTietMonAns)?
                .ThenInclude(m => m.Anhs)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
