using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class ThuongHieuDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên thương hiệu không được để trống!")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
    }
}
