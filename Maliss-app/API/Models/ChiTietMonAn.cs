using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class ChiTietMonAn
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Soluong { get; set; }
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Không được chứa ký tự đặc biệt")]
        public string? Mota { get; set; }
        public Guid? LoaiViId { get; set; }
        public Guid? NguyenLieuId { get; set; }
        
        public Guid? NhaCungCapId { get; set; }
        public Guid? KichCoId { get; set; }
        public decimal Gia { get; set; }
        public int SoNgayHSD { get; set; }
        public bool TrangThai { get; set; } = false;
        public string MonAnId { get; set; }
        public virtual MonAn? MonAn { get; set; }
        public virtual NhaCungCap? NhaCungCap { get; set; }
        public virtual NguyenLieu? NguyenLieu { get; set; }
        public virtual LoaiVi? LoaiVi { get; set; }
        public virtual KichCo? KichCo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh>? Anhs { get; set; } = new List<Anh>();
        public virtual ICollection<ChiTietCombo>? ChiTietCombos { get; set; }
        public virtual ICollection<GioHang>? GioHangs { get; set; }
        [JsonIgnore]
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
        public virtual ICollection<ChiTietGiamGia>? ChiTietGiamGias { get; set; }

    }
}
