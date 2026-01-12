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
    }
}
