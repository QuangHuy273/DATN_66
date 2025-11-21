using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class HinhThucThanhToan
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Ten { get; set; }
        public string? Mota { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
