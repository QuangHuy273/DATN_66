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
    }
}
