using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Models.DTO;
using API.Models.ViewModels;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ViewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoanController : BaseController<TaiKhoan, TaiKhoanDTO, Guid>
    {
        private readonly ITaiKhoanRepository _taikhoanrepository;
        public TaiKhoanController(ITaiKhoanRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _taikhoanrepository = repository;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.taiKhoans
                .Include(t => t.NguoiDung).ThenInclude(n => n.NhanVien).ThenInclude(cv => cv.ChucVu)
                .FirstOrDefault(t => t.UserName == model.TaiKhoan && t.Password == model.MatKhau);

            if (user == null || user.NguoiDung == null || user.NguoiDung.NhanVien == null)
                return BadRequest("Sai tài khoản hoặc mật khẩu.");

            var dto = _mapper.Map<TaiKhoanDTO>(user);
            return Ok(dto); 
        }

        [HttpPost("loginkh")]
        public IActionResult LoginKH(LoginViewModel model)
        {
            var user = _context.taiKhoans
                .Include(t => t.NguoiDung).ThenInclude(n => n.KhachHang)
                .FirstOrDefault(t => t.UserName == model.TaiKhoan && t.Password == model.MatKhau);

            if (user == null || user.NguoiDung == null || user.NguoiDung.KhachHang == null)
                return BadRequest("Sai tài khoản hoặc mật khẩu.");

            var dto = _mapper.Map<TaiKhoanKHDTO>(user);
            return Ok(dto);
        }
        [HttpPost("checktk")]
        public IActionResult CheckTk(string tk, string sdt)
        {

            if (_context.taiKhoans.Any(x => x.UserName == tk))
            {
                return BadRequest("Tên tài khoản đã tồn tại!");
            }

             
            bool sdtDaCoTaiKhoan = _context.taiKhoans
            .Include(t => t.NguoiDung).ThenInclude(n => n.KhachHang)
            .Any(t => t.NguoiDung != null && t.NguoiDung.Sdt == sdt);

            if (sdtDaCoTaiKhoan)
            {
                return BadRequest("Số điện thoại này đã có tài khoản!");
            }

            return Ok();
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddTaiKhoan( TaiKhoan model)
        {
            try
            {
                _context.taiKhoans.Add(model);
                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, false);
            }
            
        }

    }
}
