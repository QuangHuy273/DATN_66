using API.Controllers;
using API.Data;
using API.HeThong;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost("create")]
        public async Task<ActionResult<KhachHangDTO>> Create([FromBody] KhachHangDTO dto)
        {
            if (dto == null)
                return BadRequest("Dữ liệu không hợp lệ");

            // 1️⃣ Tạo NguoiDung
            var nguoiDung = new NguoiDung
            {
                Id = Guid.NewGuid(),
                Ho = dto.Ho,
                Ten = dto.Ten,
                Sdt = dto.Sdt,
                Gmail = dto.Gmail,
                NgaySinh = dto.NgaySinh
            };
            var newKhId = await _xulyId.GenerateIdAsync("KH", _context.khachHangs, "Id");
            // 2️⃣ Tạo KhachHang
            var khachHang = new KhachHang
            {
                Id = newKhId,
                TrangThai = dto.TrangThai,
                GhiChu = dto.GhiChu,
                NguoiDungId = nguoiDung.Id,
                NguoiDung = nguoiDung
            };

            // 3️⃣ Lưu vào database
            _context.nguoiDungs.Add(nguoiDung);
            _context.khachHangs.Add(khachHang);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm khách hàng: {ex.Message}");
            }

            // 4️⃣ Trả về DTO
            var resultDto = new KhachHangDTO
            {
                Id = khachHang.Id,
                Ho = nguoiDung.Ho,
                Ten = nguoiDung.Ten,
                Sdt = nguoiDung.Sdt,
                Gmail = nguoiDung.Gmail,
                NgaySinh = nguoiDung.NgaySinh,
                TrangThai = khachHang.TrangThai,
                GhiChu = khachHang.GhiChu
            };

            return Ok(resultDto);
        }

        [HttpPut("changestatus/{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var kh = await _context.khachHangs.FindAsync(id);
            if (kh == null) return NotFound();

            kh.TrangThai = !kh.TrangThai; // đảo trạng thái
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] KhachHangDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Id))
                return BadRequest("Dữ liệu không hợp lệ");

            // 🔎 1. Lấy thông tin Khách Hàng
            var kh = await _context.khachHangs.FindAsync(dto.Id);
            if (kh == null)
                return NotFound("Không tìm thấy khách hàng");

            // 🔎 2. Lấy thông tin Người Dùng
            var nd = await _context.nguoiDungs.FindAsync(kh.NguoiDungId);
            if (nd == null)
                return NotFound("Không tìm thấy người dùng của khách hàng");

            // 📝 3. Cập nhật thông tin Người Dùng
            nd.Ho = dto.Ho;
            nd.Ten = dto.Ten;
            nd.Sdt = dto.Sdt;
            nd.Gmail = dto.Gmail;
            nd.NgaySinh = dto.NgaySinh;

            // 📝 4. Cập nhật thông tin Khách Hàng
            kh.TrangThai = dto.TrangThai;
            kh.GhiChu = dto.GhiChu;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật khách hàng: {ex.Message}");
            }

            return Ok("Cập nhật thành công");

        }
        // ================================
        // 🗑️ XÓA KHÁCH HÀNG
        // ================================
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id không hợp lệ");

            // 1️⃣ Lấy khách hàng theo Id
            var kh = await _context.khachHangs.FindAsync(id);
            if (kh == null)
                return NotFound("Không tìm thấy khách hàng");

            // 2️⃣ Lấy người dùng của khách hàng
            var nd = await _context.nguoiDungs.FindAsync(kh.NguoiDungId);

            // 3️⃣ Xóa Khách Hàng
            _context.khachHangs.Remove(kh);

            // 4️⃣ Xóa Người Dùng (nếu tồn tại)
            if (nd != null)
                _context.nguoiDungs.Remove(nd);

            try
            {
                // 5️⃣ Lưu thay đổi
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa khách hàng: {ex.Message}");
            }

            return Ok("Xóa khách hàng thành công");
        }

        [HttpGet("khachhang/{id}/profile")]
        public async Task<IActionResult> GetProfileByKhachHangId(string id)
        {
            var result = await _context.khachHangs
                .Where(kh => kh.Id == id)
                .Select(kh => new KhachHangDTO
                {
                    Id = kh.Id,
                    NguoiDungId = kh.NguoiDungId,

                    Ho = kh.NguoiDung.Ho,
                    Ten = kh.NguoiDung.Ten,
                    Gmail = kh.NguoiDung.Gmail,
                    Sdt = kh.NguoiDung.Sdt,
                    NgaySinh = kh.NguoiDung.NgaySinh
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}