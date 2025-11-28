using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class DongGoiDTO
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "Tên nguyên liệu không được để trống!")]
        public string Ten { get; set; }
        public string? Mota { get; set; }
    }
}
