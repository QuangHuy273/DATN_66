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
    public class NguyenLieuController : BaseController<NguyenLieu, DongGoiDTO, Guid>
    {
        public NguyenLieuController(IRepository<NguyenLieu, Guid> repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
        }
    }
}
