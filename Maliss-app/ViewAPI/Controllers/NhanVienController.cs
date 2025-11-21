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
    public class NhanVienController : BaseController<NhanVien, NhanVienDTO, string>
    {
        private readonly INhanVienRepository _nhanvien;
        public NhanVienController(INhanVienRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _nhanvien = repository;
            _useXulyIdGeneration = true;
            _idPrefix = "NV";
            _idColumnName = "Id";
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetAll()
        {
            var result = await _nhanvien.GetAll();
            var dto = _mapper.Map<IEnumerable<NhanVienDTO>>(result);
            return Ok(dto);
        }
    }
}
