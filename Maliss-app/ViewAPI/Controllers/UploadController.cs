using API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
            [FromQuery] string? chiTiet = null,
            [FromQuery] int? chiTietIndex = null)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadPath = Path.Combine(_env.WebRootPath, "Uploads", "Images"); 

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = BuildStandardFileName(file.FileName, maMonAn, chiTiet, chiTietIndex);
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

        private static string BuildStandardFileName(string originalName, string? maMonAn, string? chiTiet, int? chiTietIndex)
        {
            var extension = Path.GetExtension(originalName);
            if (string.IsNullOrWhiteSpace(extension))
            {
                extension = ".jpg";
            }

            extension = extension.ToLowerInvariant();

            var hasMaMonAn = !string.IsNullOrWhiteSpace(maMonAn);
            var hasChiTiet = !string.IsNullOrWhiteSpace(chiTiet);

            var normalizedMa = hasMaMonAn ? NormalizeKey(maMonAn!) : null;
            var normalizedChiTiet = hasChiTiet ? NormalizeKey(chiTiet!) : null;

            // Ảnh món chính: {MaMonAn}.{ext}
            if (hasMaMonAn && !hasChiTiet)
            {
                return $"{normalizedMa}{extension}";
            }

            // Ảnh chi tiết:
            // - Trường hợp tiêu chuẩn: có cả MaMonAn và mã chi tiết
            //   => {MaMonAn}_{CT + số thứ tự chi tiết}_{index}_{timestamp}.{ext}
            if (hasMaMonAn && hasChiTiet)
            {
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var indexPart = BuildDetailIndex(chiTietIndex);

                return $"{normalizedMa}_{normalizedChiTiet}_{indexPart}_{timestamp}{extension}";
            }

            // Trường hợp chỉ có mã chi tiết (giữ lại để tương thích ngược)
            if (hasChiTiet)
            {
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var indexPart = BuildDetailIndex(chiTietIndex);
                return $"{normalizedChiTiet}{indexPart}{timestamp}{extension}";
            }

            var fallback = $"IMG_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            return $"{fallback}{extension}";
        }

        private static string BuildDetailIndex(int? chiTietIndex)
        {
            var index = Math.Max(1, (chiTietIndex ?? 0) + 1);
            return index.ToString();
        }

        private static string NormalizeKey(string value)
        {
            var trimmed = value.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                return "IMG";
            }

            var noDiacritics = RemoveDiacritics(trimmed);
            var collapsedWhitespace = Regex.Replace(noDiacritics, @"\s+", "-");
            var cleaned = Regex.Replace(collapsedWhitespace, @"[^A-Za-z0-9_-]", "-");
            cleaned = Regex.Replace(cleaned, "-{2,}", "-").Trim('-');

            return string.IsNullOrWhiteSpace(cleaned) ? "IMG" : cleaned.ToUpperInvariant();
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


    }
}
