using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class DiaChi
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}a-zA-Z0-9\s/-]+$", ErrorMessage = "Chỉ được chứa chữ cái, số, dấu cách, dấu / và -")]
        public string DiaChiCuThe { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? QuanHuyen { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Tinh { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? XaPhuong { get; set; } 
        public bool TrangThai { get; set; } = false;
        public Guid? NguoiDungId { get; set; }
        public Guid? NhaCungCapId { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
