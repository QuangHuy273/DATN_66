using API.Models.ViewModels.BanHang;

namespace API.HeThong.IHeThong
{
    public interface IXuLyDiaChi
    {
        Task<List<DiaChiNhap>> ParseDiaChiAsync(string filePath);
    }
}
