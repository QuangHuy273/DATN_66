namespace API.Models
{
    public class LichSuTrangThai
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HoaDonChiTietId { get; set; }
        public string TrangThai { get; set; }
        public DateTime ThoiGian { get; set; } = DateTime.Now;
        public string LyDo { get; set; }
        public virtual HoaDonChiTiet HoaDonChiTiet { get; set; }
    }

}
