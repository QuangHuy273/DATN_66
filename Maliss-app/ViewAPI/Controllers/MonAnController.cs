using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ViewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonAnController : BaseController<MonAn, MonAnDTO, string>
    {
        private readonly IMonAnRepository monAnRepository;
        public MonAnController(IMonAnRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _useXulyIdGeneration = true;
            _idPrefix = "MA";
            _idColumnName = "Id";
            monAnRepository = repository;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<MonAn>>> GetAll()
        {
            var result = await monAnRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<MonAnDTO>>(result);
            return Ok(dto);
        }

        /// <summary>
        /// Lấy danh sách món ăn đang hoạt động (dành cho khách hàng)
        /// Chỉ trả về các món có TrangThai = true và có ít nhất 1 chi tiết món ăn active với số lượng > 0
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<MonAnDTO>>> GetActiveProducts()
        {
            try
            {
                var allMonAn = await monAnRepository.GetAllAsync();
                
                // Lọc chỉ các món ăn có trạng thái active
                // và có ít nhất 1 chi tiết món ăn active với số lượng > 0
                var activeMonAn = allMonAn
                    .Where(m => m.TrangThai == true && 
                               m.ChiTietMonAns != null && 
                               m.ChiTietMonAns.Any(ct => ct.TrangThai == true && ct.Soluong > 0))
                    .ToList();

                var dto = _mapper.Map<IEnumerable<MonAnDTO>>(activeMonAn);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy món ăn theo ID (dành cho khách hàng)
        /// Chỉ trả về nếu món ăn đang active
        /// </summary>
        [HttpGet("active/{id}")]
        public async Task<ActionResult<MonAnDTO>> GetActiveProductById(string id)
        {
            try
            {
                var monAn = await monAnRepository.GetByIdAsync(id);
                
                if (monAn == null || monAn.TrangThai == false)
                {
                    return NotFound("Sản phẩm không tồn tại hoặc đã ngừng bán");
                }

                var dto = _mapper.Map<MonAnDTO>(monAn);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
