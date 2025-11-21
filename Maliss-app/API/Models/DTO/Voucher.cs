using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace API.Models.DTO
{
    public class Voucher
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Tên voucher không được để trống")]
        [StringLength(100, ErrorMessage = "Tên voucher không vượt quá 100 ký tự")]
        public string Ten { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Soluong { get; set; }

        [Range(1000, int.MaxValue, ErrorMessage = "Số tiền giảm phải từ 1000 trở lên")]
        public int? SoTienKhuyenMai { get; set; }

        [Range(1, 100, ErrorMessage = "Phần trăm giảm giá phải từ 1 đến 100")]
        public int? PhanTramKhuyenMai { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Đơn hàng tối thiểu không hợp lệ")]
        public int? SoTienToiThieu { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime NgayKetThuc { get; set; }

        public string? MoTa { get; set; }

        public bool ApDungSanPham { get; set; }
        public bool TrangThai { get; set; }
        public bool? Kieu { get; set; } = false;
        public int? DaSuDung { get; set; }

    }
}
