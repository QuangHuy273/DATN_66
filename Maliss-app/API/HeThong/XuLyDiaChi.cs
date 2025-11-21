
using API.HeThong.IHeThong;
using API.Models.ViewModels.BanHang;

namespace API.HeThong
{
    public class XuLyDiaChi : IXuLyDiaChi
    {
        public async Task<List<DiaChiNhap>> ParseDiaChiAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var danhSach = new List<DiaChiNhap>();
            DiaChiNhap? currentTinh = null;
            DiaChiNhap? currentHuyen = null;

            foreach (var line in lines)
            {
                var trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.Contains("-----------"))
                    continue;

                int indent = line.TakeWhile(c => c == ' ').Count();

                if (indent == 0)
                {
                    currentTinh = new DiaChiNhap { Ten = trimmed };
                    danhSach.Add(currentTinh);
                }
                else if (indent >= 2 && indent < 8 && currentTinh != null)
                {
                    currentHuyen = new DiaChiNhap { Ten = trimmed };
                    currentTinh.Con.Add(currentHuyen);
                }
                else if (indent >= 8 && currentHuyen != null)
                {
                    currentHuyen.Con.Add(new DiaChiNhap { Ten = trimmed });
                }
            }

            return danhSach;
        }
    }
}
