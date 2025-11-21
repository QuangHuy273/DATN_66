using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class NguyenLieu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        public virtual ICollection<ChiTietMonAn> ChiTietMonAn { get; set; }
    }
}
