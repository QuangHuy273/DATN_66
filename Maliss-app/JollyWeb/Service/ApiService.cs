using Azure.Core;
using System.Net.Http.Json;
using System.Text.Json;

namespace JollyWeb.Service
{
    public class ApiService : IService.IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GET] Error calling {url}: {ex.Message}");
                return default;
            }
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, data);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<TResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    Console.WriteLine($"[POST] Failed: {url} => StatusCode: {response.StatusCode} | Body: {content}");
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[POST] Exception on {url}: {ex.Message}");
                return default;
            }
        }

        public async Task<bool> PutAsync<TRequest>(string url, TRequest data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, data);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[PUT] Failed: {url} => {content}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PUT] Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync<Tkey>(string baseUrl, Tkey id)
        {
            try
            {
                var fullUrl = $"{baseUrl}/{id}";
                var response = await _httpClient.DeleteAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DELETE] Failed: {fullUrl} => {content}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DELETE] Exception: {ex.Message}");
                return false;
            }
        }
        public async Task<T?> GetByIdAsync<T, TKey>(string baseUrl, TKey id)
        {
            try
            {
                var url = $"{baseUrl}/{id}";
                return await _httpClient.GetFromJsonAsync<T>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GET BY ID] Error calling {baseUrl}/{id}: {ex.Message}");
                return default;
            }
        }

    }
}
