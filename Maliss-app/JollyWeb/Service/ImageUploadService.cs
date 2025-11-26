using JollyWeb.Service.IService;
using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
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


        public async Task<UploadResult?> UploadImageAsync(IBrowserFile file, string? maMonAn = null, string? chiTiet = null)
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

            var endpoint = BuildUploadEndpoint(maMonAn, chiTiet);
            var response = await _httpClient.PostAsync(endpoint, content);

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

        private static string BuildUploadEndpoint(string? maMonAn, string? chiTiet)
        {
            var queryParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(maMonAn))
            {
                queryParts.Add($"maMonAn={Uri.EscapeDataString(maMonAn)}");
            }

            if (!string.IsNullOrWhiteSpace(chiTiet))
            {
                queryParts.Add($"chiTiet={Uri.EscapeDataString(chiTiet)}");
            }

            var query = queryParts.Count > 0 ? $"?{string.Join("&", queryParts)}" : string.Empty;
            return $"upload/image{query}";
        }

    }
}
