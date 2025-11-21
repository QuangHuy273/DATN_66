using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.ViewModels.BanHang
{
    public class ProductOrder
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public string? TheLoai { get; set; }
        public string? DongGoi { get; set; }
        public string? ThuongHieu { get; set; }
        public string? NhaCungCap { get; set; }
        public string MonAnId { get; set; }
        public float Gia { get; set; }
        public float GiaGiam { get; set; }
        public int Soluong { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; } = false;
        public virtual ICollection<Anh> Anhs { get; set; } = new List<Anh>();
        [NotMapped]
        public List<Anh> DanhSachAnh { get; set; } = new();
        public int SoLuongDat { get; set; } = 1;
    }
}
