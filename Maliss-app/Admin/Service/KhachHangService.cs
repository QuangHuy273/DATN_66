
using Admin.Service.IService;
using API.Models.DTO;

namespace Admin.Service
{
    public class KhachHangService : ApiService, IKhachHangService
    {
        private readonly HttpClient _httpclient;
        public KhachHangService(HttpClient httpClient) : base(httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<KhachHangDTO>> GetAll()
        {
            try
            {
                var response = await _httpclient.GetAsync("KhachHang/all");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<KhachHangDTO>>();
                    return data ?? new List<KhachHangDTO>();
                }
                return new List<KhachHangDTO>();
            }
            catch (Exception)
            {
                return new List<KhachHangDTO>();
            }
        }
    }
}
