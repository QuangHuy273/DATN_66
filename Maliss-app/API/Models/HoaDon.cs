using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class HoaDon
    {
        [Key]
        public string Id { get; set; }
        public string LoaiHoaDon { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn 0")]
        public decimal TongTien { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public decimal TongTienSauKhiGiam { get; set; }
        public string TrangThai { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? GhiChu { get; set; }
        public string? GiamGiaId { get; set; }
        public string? NhanVienId { get; set; }
        public string? KhachHangId { get; set; }
        public string? DiaChi { get; set; }
        public Guid? HinhThucThanhToanId { get; set; }
        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual GiamGia GiamGia { get; set; }
        [JsonIgnore]
        public virtual ICollection<HoaDonChiTiet> hoaDonChiTiets { get; set; }


    }
}
