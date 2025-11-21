using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace API.Models.DTO
{
    public class KhachHangDTO
    {
        public string? Id { get; set; }
        public string? Ho { get; set; }
        public string? Ten { get; set; }
        public string? HoTen => $"{Ho ?? ""} {Ten ?? ""}".Trim();
        public DateTime NgaySinh { get; set; } 
        public string? Sdt { get; set; }
        public string? Gmail { get; set; }
        public bool TrangThai { get; set; } = true;
        public string? GhiChu { get; set; }
        public Guid NguoiDungId { get; set; }
        public virtual ICollection<DiaChiDTO>? DiaChis { get; set; }

    }
}
