using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class NhaCungCapDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống!")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
    }
}
