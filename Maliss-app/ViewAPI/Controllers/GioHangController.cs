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
    public class GioHangController : BaseController<GioHang, GioHangDTO, string>
    {
        public GioHangController(IRepository<GioHang, string> repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _useXulyIdGeneration = true;
            _idPrefix = "GH";
            _idColumnName = "Id";
        }
    }
}
