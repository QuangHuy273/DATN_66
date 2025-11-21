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
    public class GiamGiaController : BaseController<GiamGia, Voucher, string>
    {
        public GiamGiaController(IRepository<GiamGia, string> repository, DBAppContext context, IMapper mapper, XulyId xulyId) 
            : base(repository, context, mapper, xulyId)
        {
            _useXulyIdGeneration = true;
            _idPrefix = "VC";
            _idColumnName = "Id";
        }
    }
}
