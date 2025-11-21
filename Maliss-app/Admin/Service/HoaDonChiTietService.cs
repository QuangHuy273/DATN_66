using Admin.Service.IService;
using API.Models.DTO;

namespace Admin.Service
{
    public class HoaDonChiTietService : ApiService, IHoaDonChiTietService
    {
        private readonly HttpClient _httpclient;
        public HoaDonChiTietService(HttpClient httpClient) : base(httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<HoaDonChiTietDTO>> GetChiTiet(string Id)
        {
            try
            {
                var response = await _httpclient.GetAsync($"HoaDonChiTiet/hdct/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<HoaDonChiTietDTO>>();
                    return data ?? new List<HoaDonChiTietDTO>();
                }
                return new List<HoaDonChiTietDTO>();
            }
            catch (Exception)
            {
                return new List<HoaDonChiTietDTO>();
            }
        }
    }
}
