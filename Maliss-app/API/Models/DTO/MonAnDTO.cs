namespace API.Models.DTO
{
    public class MonAnDTO
    {
        public string? Id { get; set; }
        public required string Ten { get; set; }
        public Guid? TheLoaiId { get; set; }
        public string? TheLoai { get; set; }
        public Guid? ThuongHieuId { get; set; }
        public string? ThuongHieu { get; set; }
        public TimeOnly? HanSuDung { get; set; }
        public bool TrangThai { get; set; } 
        public string? Mota { get; set; }
        public string? AnhDaTai { get; set; }
        public decimal Gia { get; set; }
    }
}
