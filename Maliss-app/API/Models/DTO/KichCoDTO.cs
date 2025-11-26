using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class KichCoDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên kích cỡ không được để trống!")]
        public string Ten { get; set; } = string.Empty;
        public string? Mota { get; set; }
    }
}

