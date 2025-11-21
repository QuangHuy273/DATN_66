namespace API.Models.DTO
{
    public class GioHangDTO
    {
        public string? Id { get; set; }
        public int Soluong { get; set; }
        public decimal TongGia { get; set; }
        public string? ComboId { get; set; }
        public Guid? ChiTietMonAnId { get; set; }
        public string KhachHangid { get; set; }
    }
}
