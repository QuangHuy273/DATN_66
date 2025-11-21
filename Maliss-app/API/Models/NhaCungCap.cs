using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class NhaCungCap
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public required string Ten { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        public virtual ICollection<DiaChi> DiaChis { get; set; }
        
    }
}
