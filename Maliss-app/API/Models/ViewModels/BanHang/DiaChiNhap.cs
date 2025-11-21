namespace API.Models.ViewModels.BanHang
{
    public class DiaChiNhap
    {
        public string Ten { get; set; }
        public List<DiaChiNhap> Con { get; set; } = new();
    }
}
