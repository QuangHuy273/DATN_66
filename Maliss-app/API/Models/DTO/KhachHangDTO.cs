using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    /// <summary>
    /// DTO dùng cho quản lý khách hàng ở cả API và Admin.
    /// Được gắn DataAnnotations để validate phía server và phía Blazor.
    /// </summary>
    public class KhachHangDTO
    {
        public string? Id { get; set; }

        //[Required(ErrorMessage = "Họ không được để trống")]
        [StringLength(50, ErrorMessage = "Họ không được vượt quá 50 ký tự")]
        public string? Ho { get; set; }

        //[Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự")]
        public string? Ten { get; set; }

        public string? HoTen => $"{Ho ?? ""} {Ten ?? ""}".Trim();

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        //[Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string? Sdt { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Gmail { get; set; }

        public bool TrangThai { get; set; } = true;

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? GhiChu { get; set; }

        public Guid NguoiDungId { get; set; }

        public virtual ICollection<DiaChiDTO>? DiaChis { get; set; }
    }
}
