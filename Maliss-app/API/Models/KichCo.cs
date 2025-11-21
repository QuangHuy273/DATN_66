using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class KichCo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten { get; set; }
        public string Mota { get; set; }
        public ICollection<ChiTietMonAn>? chiTietMonAns { get; set; }
    }
}
