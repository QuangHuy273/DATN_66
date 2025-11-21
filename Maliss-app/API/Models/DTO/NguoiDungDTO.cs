namespace API.Models.DTO
{
    public class NguoiDungDTO
    {
        public Guid Id { get; set; } 
        public string Ho { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? Gmail { get; set; }
        public string? Sdt { get; set; }
    }
}
