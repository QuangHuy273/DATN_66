using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class LoaiViDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên loại vị không được để trống!")]
        public string Ten { get; set; } = string.Empty;
        public string? Mota { get; set; }
    }
}

