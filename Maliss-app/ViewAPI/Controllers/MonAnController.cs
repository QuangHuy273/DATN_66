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
    }
}
