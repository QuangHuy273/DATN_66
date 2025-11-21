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
    public class HoaDonController : BaseController<HoaDon, HoaDonDTO, string>
    {
        private readonly IHoaDonRepository hoaDonRepository;
        public HoaDonController(IHoaDonRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _useXulyIdGeneration = true;
            _idPrefix = "HD";
            _idColumnName = "Id";
            hoaDonRepository = repository;
        }
        [HttpGet("lichsu/{id}")]
        public async Task<ActionResult<IEnumerable<LichSuTrangThai>>> GetAll(string id)
        {
            var result = await hoaDonRepository.GetLichSu(id);
            return Ok(result);
        }
    }
}
