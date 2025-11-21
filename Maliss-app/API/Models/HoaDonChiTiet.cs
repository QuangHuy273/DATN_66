using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class HoaDonChiTiet
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Soluong { get; set; }
        public bool TrangThai { get; set; } = true;
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
        public decimal ThanhTien { get; set; }
        public string HoaDonId { get; set; }
        public Guid? ChiTietMonAnId { get; set; }
        public string? ComboId { get; set; }
        public virtual Combo Combo { get; set; }
        public virtual ChiTietMonAn ChiTietMonAn { get; set; }    
        public virtual HoaDon HoaDon { get; set; }
        [JsonIgnore]
        public virtual ICollection<LichSuTrangThai> lichSuTrangThais { get; set; }
    }
}
