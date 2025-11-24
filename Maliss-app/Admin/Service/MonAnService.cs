using Admin.Service.IService;
using API.Models.DTO;
using System.Net.Http;

namespace Admin.Service
{
    public class MonAnService : ApiService, IMonAnService
    {
        private readonly HttpClient _httpClient;
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

        public async Task<MonAnDTO?> GetById(string id)
        {
            try
            {
                return await GetbyId<MonAnDTO, string>(id, "MonAn");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetIdMonAn()
        {
            var response = await _httpClient.GetAsync("MonAn/GenerateMonAnId");
            if (response.IsSuccessStatusCode)
            {
                var rawString = await response.Content.ReadAsStringAsync();
                return rawString.Trim('"');
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<MonAnDTO?> Create(MonAnDTO monAn)
        {
            return await PostAsync<MonAnDTO, MonAnDTO>("MonAn", monAn);
        }

        public async Task<bool> Update(MonAnDTO monAn)
        {
            return await PutAsync("MonAn", monAn);
        }

        public async Task<bool> Delete(string id)
        {
            return await DeleteAsync("MonAn", id);
        }
    }
}
