using Admin.Service.IService;
using API.Models;
using API.Models.DTO;
using API.Models.ViewModels;
using Azure;
using System.Net.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Admin.Service
{
    public class TaiKhoanService : ApiService, ITaiKhoanService
    {
        HttpClient _httpclient = new();
        public TaiKhoanService(HttpClient httpClient) : base(httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<TaiKhoanDTO> Login(LoginViewModel model)
        {
            try
            {
                //var userfake = new TaiKhoanDTO();
                //userfake.Id = "abc";
                //userfake.UserName = "admin";
                //userfake.Quyen = "Admin";
                //userfake.TrangThai = true;
                //return userfake;
                var response = await _httpclient.PostAsJsonAsync("TaiKhoan/login", model);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response JSON: " + content);
                if (string.IsNullOrWhiteSpace(content))
                    return null;

                return JsonSerializer.Deserialize<TaiKhoanDTO>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch
            {
                return default;
            }
        }
    }
}
