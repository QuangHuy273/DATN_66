using Admin.Service.IService;
using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Admin.Service
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

            // Đảm bảo chỉ lấy tên file, loại bỏ query string nếu có
            var cleanFileName = ExtractFileName(fileName);
            
            var response = await _httpClient.DeleteAsync($"upload/tamthoi?fileName={Uri.EscapeDataString(cleanFileName)}");
            return response.IsSuccessStatusCode;
        }

        private static string ExtractFileName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Nếu là URL, lấy phần path và extract filename
            if (Uri.TryCreate(input, UriKind.Absolute, out var uri))
            {
                return Path.GetFileName(uri.LocalPath);
            }

            // Nếu có query string, loại bỏ nó
            var queryIndex = input.IndexOf('?');
            if (queryIndex >= 0)
            {
                input = input.Substring(0, queryIndex);
            }

            return Path.GetFileName(input);
        }


        public async Task<UploadResult?> UploadImageAsync(IBrowserFile file, string? maMonAn = null, string? chiTiet = null, int? chiTietIndex = null)
        {
            var content = new MultipartFormDataContent();
            var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); 

            var fileContent = new StreamContent(stream)
            {
                Headers =
                {
                    ContentLength = file.Size,
                    ContentType = new MediaTypeHeaderValue(file.ContentType)
                }
            };

            content.Add(fileContent, "file", file.Name);

            var endpoint = BuildUploadEndpoint(maMonAn, chiTiet, chiTietIndex);
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

        private static string BuildUploadEndpoint(string? maMonAn, string? chiTiet, int? chiTietIndex)
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

            if (chiTietIndex.HasValue)
            {
                queryParts.Add($"chiTietIndex={chiTietIndex.Value}");
            }

            var query = queryParts.Count > 0 ? $"?{string.Join("&", queryParts)}" : string.Empty;
            return $"upload/image{query}";
        }

    }
}
