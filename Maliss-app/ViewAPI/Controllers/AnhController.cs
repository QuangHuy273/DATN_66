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
    public class AnhController : BaseController<Anh , AnhDTO , Guid>
    {

        public AnhController(
            IRepository<Anh , Guid> anhrepository,
            DBAppContext context,
            IMapper mapper,
            XulyId xulyId,
            IWebHostEnvironment env
            ) : base(anhrepository, context, mapper, xulyId) 
        {
           
        }
        [HttpPost("upload-many")]
        public async Task<IActionResult> UploadMultipleImages([FromBody] List<AnhDTO> danhSachAnh)
        {
            if (danhSachAnh == null || !danhSachAnh.Any())
                return BadRequest("Không có ảnh nào được gửi lên.");

            var danhSachModel = _mapper.Map<List<Anh>>(danhSachAnh);
            foreach (var anh in danhSachModel)
            {
                anh.Id = Guid.NewGuid(); 
                await _repository.AddAsync(anh);
            }

            return Ok(_mapper.Map<List<AnhDTO>>(danhSachModel));
        }

        

    }
}
