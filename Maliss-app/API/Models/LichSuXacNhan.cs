namespace API.Models
{
    public class LichSuXacNhan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HoaDonId { get; set; }
        public DateTime ThoiGian { get; set; } = DateTime.Now;
    }
}
