using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class KichCoDTO
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "Tên kích cỡ không được để trống!")]
        public string Ten { get; set; } = string.Empty;
        public string? Mota { get; set; }
    }
}

