using API.Models.DTO;
using JollyWeb.Service.IService;
using System.Net.Http;

namespace JollyWeb.Service
{
    public class MonAnService : ApiService, IMonAnService
    {
        HttpClient _httpClient;
        public MonAnService(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MonAnDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("MonAn/all");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<MonAnDTO>>();
                    return data ?? new List<MonAnDTO>();
                }
                return new List<MonAnDTO>();
            }
            catch (Exception)
            {
                return new List<MonAnDTO>();
            }
        }

        public async Task<string> GetIdMonAn()
        {

            var response = await _httpClient.GetAsync("MonAn/GenerateMonAnId");
            if (response.IsSuccessStatusCode)
            {
                var rawString = await response.Content.ReadAsStringAsync();
                return rawString;
            }
            else
            {
                return default;
            }
        }

        public async Task<List<MonAnDTO>> GetActiveProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync("MonAn/active");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<MonAnDTO>>();
                    return data ?? new List<MonAnDTO>();
                }
                return new List<MonAnDTO>();
            }
            catch (Exception)
            {
                return new List<MonAnDTO>();
            }
        }

        public async Task<MonAnDTO?> GetActiveProductById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"MonAn/active/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<MonAnDTO>();
                    return data;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MonAnFilterResponse?> FilterForCustomer(MonAnFilterRequest filter)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("MonAn/filter-customer", filter);
                
                // Log response để debug
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[FilterForCustomer] Status: {response.StatusCode}");
                Console.WriteLine($"[FilterForCustomer] Response: {responseBody}");
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<MonAnFilterResponse>();
                    Console.WriteLine($"[FilterForCustomer] Success! Total: {data?.TotalCount ?? 0} products");
                    return data;
                }
                else
                {
                    Console.WriteLine($"[FilterForCustomer] Failed! Status: {response.StatusCode}, Body: {responseBody}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FilterForCustomer] Exception: {ex.Message}");
                Console.WriteLine($"[FilterForCustomer] StackTrace: {ex.StackTrace}");
                return null;
            }
        }
    }
}
