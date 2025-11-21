namespace API.Models.DTO
{
    public class ChiTietComboDTO
    {
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public string? ComboId { get; set; }
        public Guid? ChiTietMonAnId { get; set; }
    }
}
