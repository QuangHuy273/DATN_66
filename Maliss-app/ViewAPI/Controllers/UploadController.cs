using API.Models;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadPath = Path.Combine(_env.WebRootPath, "Uploads", "Images"); 

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
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

    }
}
