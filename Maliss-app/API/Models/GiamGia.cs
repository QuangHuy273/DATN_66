using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class GiamGia
    {
        [Key]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int? Soluong { get; set; }
        public decimal? SoTienKhuyenMai { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Phần trăm khuyến mãi phải lớn hơn 0")]
        public int? PhanTramKhuyenMai { get; set; }
        public decimal? SoTienToiThieu { get; set; }
        public decimal? GiaTriToiDa { get; set; }
        [NotFutureDate(ErrorMessage = "Ngày bắt đầu không được lớn hơn ngày hôm nay")]
        public DateTime? NgayBatDau { get; set; } = DateTime.Now;
        public DateTime? NgayKetThuc { get; set; }
        public string? MoTa { get; set; }
        public bool? Kieu { get; set; }
        public bool ApDungSanPham { get; set; }
        public bool TrangThai { get; set; } = false;
        public bool DotGiamGia { get; set; } 
        public virtual ICollection<ChiTietGiamGia> chiTietGiamGias { get; set; }
        public virtual ICollection<HoaDon> hoaDons { get; set; }
    }
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dt)
            {
                return dt <= DateTime.Today;
            }
            return true; 
        }
    }
}
