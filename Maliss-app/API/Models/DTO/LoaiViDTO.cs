using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class LoaiViDTO
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "Tên loại vị không được để trống!")]
        public string Ten { get; set; } = string.Empty;
        public string? Mota { get; set; }
    }
}

