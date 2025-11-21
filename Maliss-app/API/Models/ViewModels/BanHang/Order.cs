using API.Models.DTO;

namespace API.Models.ViewModels.BanHang
{
    public class Order
    {
        public List<ChiTietMonAnDTO> Products { get; set; } = new();
        public KhachHangDTO? Customer { get; set; }
        public DiaChiDTO? Address { get; set; }
        public ThongTinHoaDon ThongTinHoaDon { get; set; } = new();

        public DateTime ThoiGianTao { get; set; } = DateTime.Now;

    }
}
