namespace API.Models.ViewModels.Thongke
{
    public class DanhSachSanPhamBanChay
    {
        public string TenSanPham { get; set; }
        public string Anh { get; set; }
        public int SoDonHang { get; set; }  // ⭐ THÊM MỚI
        public int SoLuong { get; set; }
        public decimal GiaTien { get; set; }
        public string KichCo { get; set; }
    }
}
