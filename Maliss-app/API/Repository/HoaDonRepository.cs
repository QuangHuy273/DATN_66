
using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace API.Repository
{
    public class HoaDonRepository : Repository<HoaDon, string>, IHoaDonRepository
    {
        private readonly DBAppContext _context;

        public HoaDonRepository(DBAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateOrderStatus(Guid hoaDonChiTietId, string newStatus, string reason)
        {
            var orderDetail = await _context.hoaDonChiTiets.FindAsync(hoaDonChiTietId);
            if (orderDetail != null)
            {
                orderDetail.TrangThai = (newStatus == "Hoàn thành");
                await _context.SaveChangesAsync();

                var history = new LichSuTrangThai
                {
                    HoaDonChiTietId = hoaDonChiTietId,
                    TrangThai = newStatus,
                    LyDo = reason,
                    ThoiGian = DateTime.Now
                };
                _context.trangThais.Add(history);
                await _context.SaveChangesAsync();

                var hoaDon = await _context.hoaDons
                    .FirstOrDefaultAsync(h => h.Id == orderDetail.HoaDonId);

                if (hoaDon != null)
                {
                    hoaDon.TrangThai = newStatus;

                    await _context.SaveChangesAsync();
                }
            }
        }


        public async Task<List<LichSuTrangThai>> GetLichSu(string hoaDonId)
        {
            var data = await _context.trangThais
                .Include(x => x.HoaDonChiTiet)
                .ThenInclude(ct => ct.ChiTietMonAn)
                .Where(x => x.HoaDonChiTiet.HoaDonId == hoaDonId)
                .OrderByDescending(x => x.ThoiGian)
                .ToListAsync();
            return data;
        }
    }
}
