using Admin.Service.IService;
using API.Models.DTO;

namespace Admin.Service
{
    public class NhanVienService : ApiService, INhanVienService
    {
        private readonly HttpClient _httpclient;
        public NhanVienService(HttpClient httpClient) : base(httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<NhanVienDTO>> GetAll()
        {
            try
            {
                var response = await _httpclient.GetAsync("NhanVien/all");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<NhanVienDTO>>();
                    return data ?? new List<NhanVienDTO>();
                }
                return new List<NhanVienDTO>();
            }
            catch (Exception)
            {
                return new List<NhanVienDTO>();
            }
        }
    }
}
