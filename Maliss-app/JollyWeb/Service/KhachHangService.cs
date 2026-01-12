using JollyWeb.Service.IService;
using API.Models.DTO;

namespace JollyWeb.Service
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
        public async Task<KhachHangDTO?> GetById(string id)
        {
            try
            {
                var response = await _httpclient.GetAsync($"KhachHang/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<KhachHangDTO>();
            }
            catch
            {
                return null;
            }
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
        public async Task<KhachHangDTO?> GetProfile(string khachHangId)
        {
            var response = await _httpclient.GetAsync($"KhachHang/khachhang/{khachHangId}/profile");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<KhachHangDTO>();
        }


    }
}