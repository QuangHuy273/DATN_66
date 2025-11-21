using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class ThuongHieu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public required string Ten { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        [JsonIgnore]
        public virtual ICollection<MonAn>? MonAns { get; set; }
    }
}
