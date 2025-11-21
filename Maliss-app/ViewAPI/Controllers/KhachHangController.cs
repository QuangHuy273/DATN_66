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
    public class KhachHangController : BaseController<KhachHang, KhachHangDTO, string>
    {
        private readonly IKhachHangRepository _khachhang;
        public KhachHangController(IKhachHangRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _khachhang = repository;
            _useXulyIdGeneration = true;
            _idPrefix = "KH";
            _idColumnName = "Id";
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetAll()
        {
            var result = await _khachhang.GetAll();
            var dto = _mapper.Map<IEnumerable<KhachHangDTO>>(result);
            return Ok(dto);
        }
    }
}
