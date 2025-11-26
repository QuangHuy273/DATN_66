using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ViewAPI.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(
            IFormFile file,
            [FromQuery] string? maMonAn = null,
            [FromQuery] string? chiTiet = null)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadPath = Path.Combine(_env.WebRootPath, "Uploads", "Images"); 

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = BuildFriendlyFileName(file.FileName, maMonAn, chiTiet);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/Uploads/Images/{fileName}";

            return Ok(new UploadResult
            {
                FileName = fileName,
                Url = url
            });
        }

        [HttpDelete("tamthoi")]
        public IActionResult XoaAnhTamThoi([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Thiếu tên file");

            var folderPath = Path.Combine(_env.WebRootPath, "Uploads", "Images");

            if (!Directory.Exists(folderPath))
                return NotFound("Thư mục ảnh không tồn tại");

            var filePath = Path.Combine(folderPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Ảnh không tồn tại");

            try
            {
                System.IO.File.Delete(filePath);
                return Ok(new { message = $"Xóa ảnh `{fileName}` thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa ảnh: {ex.Message}");
            }
        }

        private static string BuildFriendlyFileName(string originalName, string? maMonAn, string? chiTiet)
        {
            var extension = Path.GetExtension(originalName);
            if (string.IsNullOrWhiteSpace(extension))
            {
                extension = ".jpg";
            }

            var segments = new List<string>();

            var monAnSegment = SanitizeSegment(maMonAn);
            if (!string.IsNullOrEmpty(monAnSegment))
            {
                segments.Add($"mon-{monAnSegment}");
            }

            var chiTietSegment = SanitizeSegment(chiTiet);
            if (!string.IsNullOrEmpty(chiTietSegment))
            {
                segments.Add($"ct-{chiTietSegment}");
            }

            if (!segments.Any())
            {
                var fallback = SanitizeSegment(Path.GetFileNameWithoutExtension(originalName));
                segments.Add(string.IsNullOrEmpty(fallback) ? "anh" : fallback);
            }

            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            segments.Add(timestamp);

            var baseName = string.Join("_", segments);
            return $"{baseName}{extension.ToLowerInvariant()}";
        }

        private static string SanitizeSegment(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var normalized = input
                .Trim()
                .ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);

            var builder = new StringBuilder();
            foreach (var ch in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (category == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                if (char.IsLetterOrDigit(ch))
                {
                    builder.Append(ch);
                }
                else if (ch == '-' || ch == '_')
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append('-');
                }
            }

            var sanitized = Regex.Replace(builder.ToString(), "-{2,}", "-").Trim('-');
            return sanitized;
        }

    }
}
