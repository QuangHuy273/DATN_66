
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
        public async Task<bool> Create(KhachHangDTO dto)
        {
            try
            {
                var response = await _httpclient.PostAsJsonAsync("KhachHang/create", dto);

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
            var response = await _httpclient.PutAsJsonAsync($"KhachHang/changestatus/{id}", id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(KhachHangDTO dto)
        {
            try
            {
                var response = await _httpclient.PutAsJsonAsync("KhachHang/update", dto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string id)
        {
            var response = await _httpclient.DeleteAsync($"KhachHang/delete/{id}");
            return response.IsSuccessStatusCode;
        }


    }
}
