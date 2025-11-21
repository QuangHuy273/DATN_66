using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ViewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KichCoController : BaseController<KichCo, KichCo, Guid>
    {
        public KichCoController(IRepository<KichCo, Guid> repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
        }
    }
}