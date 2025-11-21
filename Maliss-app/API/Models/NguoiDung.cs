using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class NguoiDung
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Ho { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        [EmailAddress]
        public string? Gmail { get; set; }
        [Phone]
        public string? Sdt { get; set; }
        public virtual ICollection<DiaChi>? DiaChis { get; set; }
        [JsonIgnore]
        public virtual NhanVien? NhanVien { get; set; } 
        public virtual KhachHang? KhachHang { get; set; }
    }
    
}
