using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace API.Models.DTO
{
    public class NhanVienDTO
    {
        public string? Id { get; set; }
        public string? Ho { get; set; }
        public string? Ten { get; set; }
        public string? HoTen => $"{Ho ?? ""} {Ten ?? ""}".Trim();
        public DateTime NgaySinh { get; set; }
        [Phone(ErrorMessage = "Lỗi định dạng số điện thoại!")]
        public string? Sdt { get; set; }
        [EmailAddress(ErrorMessage = "Lỗi định dạng Gmail!")]
        public string? Gmail { get; set; }
        public string? TenChucVu { get; set; }
        public bool TrangThai { get; set; }
        public string? NgayVaoLam { get; set; } 
        public Guid? ChucVuId { get; set; }
        public Guid NguoiDungId { get; set; }
        public DateTime GetNgayVaoLam()
        {
            if (DateTime.TryParseExact(NgayVaoLam, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            return DateTime.Now;
        }

    }
}
