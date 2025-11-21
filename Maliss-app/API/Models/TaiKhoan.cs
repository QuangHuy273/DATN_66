
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class TaiKhoan
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Password { get; set; }
        public DateTime NgayTaoTk { get; set; } = DateTime.Now;
        public Guid NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
