using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class NhaCungCapDTO
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống!")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
    }
}
