using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace API.Models
{
    public class MonAn
    {
        [Key]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        [Range(1, float.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]

        public bool TrangThai { get; set; } = false;
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        public Guid? TheLoaiId { get; set; }
        public Guid? ThuongHieuId { get; set; }
        public virtual ThuongHieu? ThuongHieu { get; set; }
        public virtual TheLoai? TheLoai { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChiTietMonAn>? ChiTietMonAns { get; set; }
    }
}
