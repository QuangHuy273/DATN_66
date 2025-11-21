using Admin.Service.IService;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using System.Net.Http;

namespace Admin.Service
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository hoaDonRepository;
        private readonly HttpClient _httpclient;
        public HoaDonService(IHoaDonRepository hoaDonRepository, HttpClient httpClient) 
        {
            this.hoaDonRepository = hoaDonRepository;
            this._httpclient = httpClient;
        }

        public async Task<List<LichSuTrangThai>> GetLichSu(string hoadonId)
        {
            try
            {
                var response = await _httpclient.GetAsync($"HoaDon/lichsu/{hoadonId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<LichSuTrangThai>>();
                    return data ?? new List<LichSuTrangThai>();
                }
                return new List<LichSuTrangThai>();
            }
            catch (Exception)
            {
                return new List<LichSuTrangThai>();
            }
        }

        public async Task<bool> LichSuTrangThai(Guid orderId, string newStatus, string reason)
        {
            try
            {
                await hoaDonRepository.UpdateOrderStatus(orderId, newStatus, reason);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
