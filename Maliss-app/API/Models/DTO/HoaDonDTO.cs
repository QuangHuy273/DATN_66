namespace API.Models.DTO
{
    public class HoaDonDTO
    {
        public string? Id { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public string LoaiHoaDon { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public decimal TongTienSauKhiGiam { get; set; }
        public string? GhiChu { get; set; }
        public string? GiamGiaId { get; set; }
        public string? NhanVienId { get; set; }
        public string? KhachHangId { get; set; }
        public Guid? HinhThucThanhToanId { get; set; }
        public string? DiaChi { get; set; }
    }
}
