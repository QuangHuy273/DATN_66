using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class TheLoai
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string Ten { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        [JsonIgnore]
        public virtual ICollection<MonAn>? MonAns { get; set; }
    }
}
