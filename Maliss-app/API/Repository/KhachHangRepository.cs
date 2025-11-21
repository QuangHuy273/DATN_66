using API.Data;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class KhachHangRepository : Repository<KhachHang, string>, IKhachHangRepository
    {
        private readonly DBAppContext _context;
        public KhachHangRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KhachHang>> GetAll()
        {
            var khachHang = await _context.khachHangs
            .Include(kh => kh.NguoiDung)
            .ThenInclude(nd => nd.DiaChis)
            .ToListAsync();
            return khachHang;
        }
    }
}
