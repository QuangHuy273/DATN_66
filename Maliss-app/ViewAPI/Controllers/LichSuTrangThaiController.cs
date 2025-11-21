using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ViewAPI.Controllers
{
    public class LichSuTrangThaiController : BaseController<LichSuTrangThai, LichSuTrangThai, Guid>
    {
        public LichSuTrangThaiController(IRepository<LichSuTrangThai, Guid> repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
        }

    }
}
