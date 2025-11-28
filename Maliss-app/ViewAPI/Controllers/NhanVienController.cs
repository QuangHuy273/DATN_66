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
    public class NhanVienController : BaseController<NhanVien, NhanVienDTO, string>
    {
        private readonly INhanVienRepository _nhanvien;

        public NhanVienController(INhanVienRepository repository, DBAppContext context, IMapper mapper, XulyId xulyId) : base(repository, context, mapper, xulyId)
        {
            _nhanvien = repository;
            _useXulyIdGeneration = true;
            _idPrefix = "NV";
            _idColumnName = "Id";
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetAll()
        {
            var result = await _nhanvien.GetAll();
            var dto = _mapper.Map<IEnumerable<NhanVienDTO>>(result);
            return Ok(dto);
        }
        [HttpPost("create")]
        public async Task<ActionResult<NhanVienDTO>> Create([FromBody] NhanVienDTO dto)
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
            var newKhId = await _xulyId.GenerateIdAsync("NV", _context.nhanViens, "Id");

            var chucVuId = await _context.chucVus
    .Where(x => x.Ten == dto.TenChucVu)
    .Select(x => x.Id)
    .FirstOrDefaultAsync();
            // 2️⃣ Tạo NhanVien
            var nhanVien = new NhanVien
            {
                Id = newKhId,
                TrangThai = dto.TrangThai,
                NguoiDungId = nguoiDung.Id,
                NguoiDung = nguoiDung,
                NgayVaoLam = DateTime.Parse(dto.NgayVaoLam),
                ChucVuId = chucVuId

            };

            // 3️⃣ Lưu vào database
            _context.nguoiDungs.Add(nguoiDung);
            _context.nhanViens.Add(nhanVien);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm nhân viên: {ex.Message}");
            }

            // 4️⃣ Trả về DTO
            var resultDto = new NhanVienDTO
            {
                Id = nhanVien.Id,
                Ho = nguoiDung.Ho,
                Ten = nguoiDung.Ten,
                Sdt = nguoiDung.Sdt,
                Gmail = nguoiDung.Gmail,
                NgaySinh = nguoiDung.NgaySinh,
                TrangThai = nhanVien.TrangThai  
            };

            return Ok(resultDto);
        }

        [HttpPut("changestatus/{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var kh = await _context.nhanViens.FindAsync(id);
            if (kh == null) return NotFound();

            kh.TrangThai = !kh.TrangThai; // đảo trạng thái
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] NhanVienDTO dto)
        {
            var chucVuId = await _context.chucVus
    .Where(x => x.Ten == dto.TenChucVu)
    .Select(x => x.Id)
    .FirstOrDefaultAsync();
            if (dto == null || string.IsNullOrEmpty(dto.Id))
                return BadRequest("Dữ liệu không hợp lệ");

            // 🔎 1. Lấy thông tin Nhân viên
            var nv = await _context.nhanViens.FindAsync(dto.Id);
            if (nv == null)
                return NotFound("Không tìm thấy nhân viên");

            // 🔎 2. Lấy thông tin Người Dùng
            var nd = await _context.nguoiDungs.FindAsync(nv.NguoiDungId);
            if (nd == null)
                return NotFound("Không tìm thấy người dùng của nhân viên");

            // 📝 3. Cập nhật thông tin Người Dùng
            nd.Ho = dto.Ho;
            nd.Ten = dto.Ten;
            nd.Sdt = dto.Sdt;
            nd.Gmail = dto.Gmail;
            nd.NgaySinh = dto.NgaySinh;

            // 📝 4. Cập nhật thông tin nhân viên
            nv.ChucVuId = chucVuId;
            nv.TrangThai = dto.TrangThai;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật nhân viên: {ex.Message}");
            }

            return Ok("Cập nhật thành công");
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var nv = await _context.nhanViens.FindAsync(id);
            if (nv == null) return NotFound();

            var nd = await _context.nguoiDungs.FindAsync(nv.NguoiDungId);

            _context.nhanViens.Remove(nv);
            if (nd != null) _context.nguoiDungs.Remove(nd);

            await _context.SaveChangesAsync();
            return Ok("Xóa thành công");
        }


    }
}

