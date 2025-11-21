using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Combo
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Gia { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        public Guid? LoaiId { get; set; }
        public virtual ICollection<ChiTietCombo> ChiTietCombos { get; set; } = new List<ChiTietCombo>();
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
        public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
        public virtual Loai Loai { get; set; }
    }
}
