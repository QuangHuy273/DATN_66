using JollyWeb.Service.IService;
using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JollyWeb.Service
{
    public class ImageUploadService : IUploadService
    {
        private readonly HttpClient _httpClient;

        public ImageUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteImageAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;

            var response = await _httpClient.DeleteAsync($"upload/tamthoi?fileName={fileName}" );
            return response.IsSuccessStatusCode;
        }


        public async Task<UploadResult?> UploadImageAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB

            var fileContent = new StreamContent(stream)
            {
                Headers =
                {
                    ContentLength = file.Size,
                    ContentType = new MediaTypeHeaderValue(file.ContentType)
                }
            };

            content.Add(fileContent, "file", file.Name);

            var response = await _httpClient.PostAsync("upload/image", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                Console.WriteLine("Upload thành công: " + result?.Url);
                return result;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Upload thất bại: {response.StatusCode}, Error: {error}");
            }

            return null;
        }

    }
}
