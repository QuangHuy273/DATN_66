using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietMonAnController : BaseController<ChiTietMonAn, ChiTietMonAn, Guid>
    {
        private readonly IChiTietMonAnRepository chiTietMonAn;
        public ChiTietMonAnController(IChiTietMonAnRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            chiTietMonAn = repository;
        }
        [HttpGet("monan/{id}")]
        public async Task<IActionResult> GetMonAnId(string id)
        {
            var result = await chiTietMonAn.GetMonAnId(id);
            var dto = _mapper.Map<IEnumerable<ChiTietMonAnDTO>>(result);
            return Ok(dto);
        }
        [HttpPut("GiamSoLuong/{id}")]
        public async Task<IActionResult> GiamSoLuong(Guid id, [FromQuery] int soLuongTru)
        {
            if (soLuongTru <= 0)
                return BadRequest("Số lượng trừ không hợp lệ.");

            var item = await _context.chiTietMonAns.FindAsync(id);
            if (item == null)
                return NotFound("Không tìm thấy sản phẩm.");

            if (item.Soluong < soLuongTru)
                return BadRequest("Không đủ số lượng trong kho.");

            item.Soluong -= soLuongTru;

            await _context.SaveChangesAsync();
            return Ok("Đã cập nhật số lượng.");
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ChiTietMonAnDTO>>> GetAll()
        {
            var result = await chiTietMonAn.GetAll();
            var dto = _mapper.Map<IEnumerable<ChiTietMonAnDTO>>(result);
            return Ok(dto);
        }

        /// <summary>
        /// Lấy chi tiết món ăn đang hoạt động theo MonAnId (dành cho khách hàng)
        /// Chỉ trả về các chi tiết có TrangThai = true và Soluong > 0
        /// </summary>
        [HttpGet("active/{monAnId}")]
        public async Task<ActionResult<IEnumerable<ChiTietMonAnDTO>>> GetActiveDetailsByMonAnId(string monAnId)
        {
            try
            {
                var allDetails = await chiTietMonAn.GetMonAnId(monAnId);
                
                // Lọc chỉ các chi tiết đang active và còn hàng
                var activeDetails = allDetails
                    .Where(ct => ct.TrangThai == true && ct.Soluong > 0)
                    .OrderBy(ct => ct.Gia) // Sắp xếp theo giá tăng dần
                    .ToList();

                if (!activeDetails.Any())
                {
                    return NotFound("Không tìm thấy chi tiết sản phẩm đang bán");
                }

                var dto = _mapper.Map<IEnumerable<ChiTietMonAnDTO>>(activeDetails);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy tất cả chi tiết món ăn đang hoạt động (dành cho khách hàng)
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ChiTietMonAnDTO>>> GetAllActiveDetails()
        {
            try
            {
                var allDetails = await chiTietMonAn.GetAll();
                
                var activeDetails = allDetails
                    .Where(ct => ct.TrangThai == true && 
                                ct.Soluong > 0 &&
                                ct.MonAn != null && 
                                ct.MonAn.TrangThai == true)
                    .ToList();

                var dto = _mapper.Map<IEnumerable<ChiTietMonAnDTO>>(activeDetails);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
