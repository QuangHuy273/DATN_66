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
        public async Task<bool> Create(NhanVienDTO dto)
        {
            try
            {
                var response = await _httpclient.PostAsJsonAsync("NhanVien/create", dto);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ChangeStatus(string id)
        {
            var response = await _httpclient.PutAsJsonAsync($"NhanVien/changestatus/{id}", id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(NhanVienDTO dto)
        {
            try
            {
                var response = await _httpclient.PutAsJsonAsync("NhanVien/update", dto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string id)
        {
            var res = await _httpclient.DeleteAsync($"NhanVien/delete/{id}");
            return res.IsSuccessStatusCode;
        }

    }
}