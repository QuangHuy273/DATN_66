namespace API.Models.DTO
{
    public class DiaChiDTO
    {
        public Guid Id { get; set; }
        public string DiaChiCuThe { get; set; }
        public string? QuanHuyen { get; set; }
        public string? Tinh {  get; set; }
        public string? XaPhuong { get; set; }
        public bool TrangThai { get; set; }
        public Guid? NguoiDungId { get; set; }
        public Guid? NhaCungCapId { get; set; }
    }
}
