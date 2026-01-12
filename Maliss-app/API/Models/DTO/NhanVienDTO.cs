using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    /// <summary>
    /// DTO dùng cho quản lý nhân viên, kèm DataAnnotations để validate.
    /// </summary>
    public class NhanVienDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Họ không được để trống")]
        [StringLength(50, ErrorMessage = "Họ không được vượt quá 50 ký tự")]
        public string? Ho { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự")]
        public string? Ten { get; set; }

        public string? HoTen => $"{Ho ?? ""} {Ten ?? ""}".Trim();

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string? Sdt { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Gmail { get; set; }

        [Required(ErrorMessage = "Chức vụ không được để trống")]
        [StringLength(100, ErrorMessage = "Tên chức vụ không được vượt quá 100 ký tự")]
        public string? TenChucVu { get; set; }

        public bool TrangThai { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayVaoLam { get; set; }

        public Guid? ChucVuId { get; set; }

        public Guid NguoiDungId { get; set; }
    }
}
