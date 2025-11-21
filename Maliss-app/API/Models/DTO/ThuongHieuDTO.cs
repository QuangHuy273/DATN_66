namespace API.Models.DTO
{
    public class ThuongHieuDTO
    {
        public Guid Id { get; set; } 
        public required string Ten { get; set; }
        public string? Mota { get; set; }
    }
}
