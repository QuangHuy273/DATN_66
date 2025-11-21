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
    public class HoaDonChiTietController : BaseController<HoaDonChiTiet, HoaDonChiTietDTO, Guid>
    {
        private readonly IChiTietHoaDonRepository hoaDonRepository;
        public HoaDonChiTietController(IChiTietHoaDonRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            hoaDonRepository = repository;
        }
        [HttpGet("hdct/{id}")]
        public async Task<IActionResult> GetMonAnId(string id)
        {
            var result = await hoaDonRepository.GetHoaDonId(id);
            var dto = _mapper.Map<IEnumerable<HoaDonChiTietDTO>>(result);
            return Ok(dto);
        }
    }
}
