using JollyWeb.Service.IService;
using API.Models;
using API.Models.DTO;
using API.Models.ViewModels;
using Azure;
using System.Net.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JollyWeb.Service
{
    public class TaiKhoanService : ApiService, ITaiKhoanService
    {
        HttpClient _httpclient = new();
        public TaiKhoanService(HttpClient httpClient) : base(httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<TaiKhoanKHDTO> Login(LoginViewModel model)
        {
            try
            {
                var response = await _httpclient.PostAsJsonAsync("TaiKhoan/loginkh", model);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response JSON: " + content);
                if (string.IsNullOrWhiteSpace(content))
                    return null;

                return JsonSerializer.Deserialize<TaiKhoanKHDTO>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch
            {
                return default;
            }
        }
        public async Task<string?> CheckTk(string username, string sdt)
        {
            try
            {
                var url = $"TaiKhoan/checktk?tk={username}&sdt={sdt}";

                var response = await _httpclient.PostAsync(url, null); 
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return content; 
                }
                else
                {
                    return content;
                }
            }
            catch (Exception ex)
            {
                return $"Lỗi khi gọi API: {ex.Message}";
            }
        }
        public async Task<bool> AddTaiKhoanAsync(TaiKhoan model)
        {
            try
            {
                var response = await _httpclient.PostAsJsonAsync($"TaiKhoan/add", model);

                if (!response.IsSuccessStatusCode)
                    return false;

                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
