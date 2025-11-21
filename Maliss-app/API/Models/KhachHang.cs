using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class KhachHang
    {
        [Key]   
        public string Id { get; set; }
        public bool TrangThai { get; set; } = false;
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? GhiChu { get; set; }
        public Guid NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<GioHang>? GioHangs { get; set; }
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
    }
}
