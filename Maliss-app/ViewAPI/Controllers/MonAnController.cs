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

        /// <summary>
        /// Lấy danh sách món ăn đang hoạt động (TrangThai = true) - Dành cho khách hàng
        /// Chỉ hiển thị món ăn có ít nhất 1 chi tiết với hạn sử dụng >= 7 ngày so với hiện tại
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<MonAnDTO>>> GetActiveProducts()
        {
            try
            {
                // Tính ngày tối thiểu cho hạn sử dụng: hiện tại + 7 ngày
                var minExpiryDate = DateTime.Now.AddDays(7);

                var activeProducts = await _context.monAns
                    .Where(m => m.TrangThai == true &&
                                // Món ăn phải có ít nhất 1 chi tiết thỏa điều kiện:
                                // - Đang hoạt động
                                // - Còn hàng
                                // - Hạn sử dụng >= 7 ngày
                                m.ChiTietMonAns.Any(ct => 
                                    ct.TrangThai == true && 
                                    ct.Soluong > 0 && 
                                    ct.HanSuDung >= minExpiryDate))
                    .Include(m => m.TheLoai)
                    .Include(m => m.ThuongHieu)
                    .Include(m => m.ChiTietMonAns)
                        .ThenInclude(ct => ct.Anhs)
                    .OrderBy(m => m.Ten)
                    .ToListAsync();

                var dto = _mapper.Map<IEnumerable<MonAnDTO>>(activeProducts);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi lấy danh sách món ăn: {ex.Message}" });
            }
        }

        /// <summary>
        /// Lấy món ăn theo ID nếu đang hoạt động (TrangThai = true)
        /// </summary>
        [HttpGet("active/{id}")]
        public async Task<ActionResult<MonAnDTO>> GetActiveProductById(string id)
        {
            try
            {
                var product = await _context.monAns
                    .Where(m => m.Id == id && m.TrangThai == true)
                    .Include(m => m.TheLoai)
                    .Include(m => m.ThuongHieu)
                    .Include(m => m.ChiTietMonAns)
                        .ThenInclude(ct => ct.Anhs)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new { message = "Không tìm thấy món ăn hoặc món ăn đã ngừng hoạt động" });
                }

                var dto = _mapper.Map<MonAnDTO>(product);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi lấy thông tin món ăn: {ex.Message}" });
            }
        }

        /// <summary>
        /// API lọc và phân trang sản phẩm dành cho KHÁCH HÀNG
        /// - Chỉ hiển thị sản phẩm đang hoạt động (TrangThai = true)
        /// - Chỉ hiển thị sản phẩm có ít nhất 1 biến thể hợp lệ (HSD >= 7 ngày, còn hàng, đang hoạt động)
        /// - Sử dụng MonAnFilterRequest (DTO chung với Admin)
        /// - Phân trang server-side
        /// </summary>
        [HttpPost("filter-customer")]
        public async Task<ActionResult<MonAnFilterResponse>> FilterForCustomer([FromBody] MonAnFilterRequest filter)
        {
            try
            {
                Console.WriteLine($"[FilterForCustomer] Received request:");
                Console.WriteLine($"  - PageIndex: {filter.PageIndex}");
                Console.WriteLine($"  - PageSize: {filter.PageSize}");
                Console.WriteLine($"  - Keyword: {filter.Keyword}");
                Console.WriteLine($"  - TrangThaiMonAn: {filter.TrangThaiMonAn}");
                Console.WriteLine($"  - TheLoaiId: {filter.TheLoaiId}");
                Console.WriteLine($"  - ThuongHieuId: {filter.ThuongHieuId}");
                Console.WriteLine($"  - GiaTo: {filter.GiaTo}");
                
                // 1. Validate và thiết lập giá trị mặc định cho phân trang
                var pageIndex = filter.PageIndex ?? 1;
                var pageSize = filter.PageSize ?? 12;
                
                if (pageIndex <= 0) pageIndex = 1;
                if (pageSize <= 0 || pageSize > 50) pageSize = 12; // Giới hạn tối đa 50 items/trang

                // 2. Tính ngày tối thiểu cho hạn sử dụng: hiện tại + 7 ngày
                var minExpiryDate = DateTime.Now.AddDays(7);
                Console.WriteLine($"[FilterForCustomer] Min expiry date: {minExpiryDate:yyyy-MM-dd}");

                // 3. Tạo query món ăn từ database (KHÔNG LOAD TOÀN BỘ)
                // BẮT BUỘC: Chỉ lấy sản phẩm đang hoạt động + có biến thể hợp lệ
                IQueryable<MonAn> monAnQuery = _context.monAns
                    .Where(m => m.TrangThai == true &&
                                // Món ăn phải có ít nhất 1 chi tiết thỏa điều kiện:
                                // - Đang hoạt động
                                // - Còn hàng
                                // - Hạn sử dụng >= 7 ngày
                                m.ChiTietMonAns.Any(ct => 
                                    ct.TrangThai == true && 
                                    ct.Soluong > 0 && 
                                    ct.HanSuDung >= minExpiryDate))
                    .Include(m => m.TheLoai)
                    .Include(m => m.ThuongHieu)
                    .Include(m => m.ChiTietMonAns)
                        .ThenInclude(ct => ct.Anhs);

                // 4. Filter theo Keyword (tìm kiếm)
                if (!string.IsNullOrWhiteSpace(filter.Keyword))
                {
                    var keyword = filter.Keyword.ToLower().Trim();
                    monAnQuery = monAnQuery.Where(m =>
                        (m.Ten != null && m.Ten.ToLower().Contains(keyword)) ||
                        (m.Mota != null && m.Mota.ToLower().Contains(keyword))
                    );
                }

                // 5. Filter theo thể loại (danh mục)
                if (filter.TheLoaiId.HasValue)
                {
                    monAnQuery = monAnQuery.Where(m => m.TheLoaiId == filter.TheLoaiId.Value);
                }

                // 6. Filter theo thương hiệu
                if (filter.ThuongHieuId.HasValue)
                {
                    monAnQuery = monAnQuery.Where(m => m.ThuongHieuId == filter.ThuongHieuId.Value);
                }

                // 7. Filter theo khoảng giá (kiểm tra giá của các biến thể)
                if (filter.GiaFrom.HasValue || filter.GiaTo.HasValue)
                {
                    monAnQuery = monAnQuery.Where(m => m.ChiTietMonAns.Any(ct =>
                        ct.TrangThai == true &&
                        ct.Soluong > 0 &&
                        ct.HanSuDung >= minExpiryDate &&
                        (!filter.GiaFrom.HasValue || ct.Gia >= filter.GiaFrom.Value) &&
                        (!filter.GiaTo.HasValue || ct.Gia <= filter.GiaTo.Value)
                    ));
                }

                // 8. Sắp xếp theo yêu cầu
                var sortBy = filter.SortBy?.ToLower() ?? "newest";
                Console.WriteLine($"[FilterForCustomer] SortBy: {sortBy}");
                
                switch (sortBy)
                {
                    case "oldest":
                        monAnQuery = monAnQuery.OrderBy(m => m.Id);
                        break;
                    case "price-asc":
                        // Sắp xếp theo giá thấp nhất của các chi tiết hợp lệ
                        monAnQuery = monAnQuery.OrderBy(m => 
                            m.ChiTietMonAns
                                .Where(ct => ct.TrangThai == true && ct.Soluong > 0 && ct.HanSuDung >= minExpiryDate)
                                .Min(ct => (decimal?)ct.Gia) ?? decimal.MaxValue);
                        break;
                    case "price-desc":
                        // Sắp xếp theo giá cao nhất của các chi tiết hợp lệ
                        monAnQuery = monAnQuery.OrderByDescending(m => 
                            m.ChiTietMonAns
                                .Where(ct => ct.TrangThai == true && ct.Soluong > 0 && ct.HanSuDung >= minExpiryDate)
                                .Max(ct => (decimal?)ct.Gia) ?? 0);
                        break;
                    case "newest":
                    default:
                        monAnQuery = monAnQuery.OrderByDescending(m => m.Id);
                        break;
                }

                // 9. Đếm tổng số món ăn sau filter (TRƯỚC KHI PHÂN TRANG)
                var totalCount = await monAnQuery.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                
                Console.WriteLine($"[FilterForCustomer] Total products found: {totalCount}");
                Console.WriteLine($"[FilterForCustomer] Total pages: {totalPages}");

                // 10. PHÂN TRANG NGAY TẠI DATABASE (Skip/Take)
                var pagedMonAns = await monAnQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                    
                Console.WriteLine($"[FilterForCustomer] Products in current page: {pagedMonAns.Count}");

                // 11. Map sang DTO (đơn giản, không cần tính matching variants)
                var monAnWithVariants = pagedMonAns.Select(monAn => new MonAnWithMatchingVariants
                {
                    MonAn = _mapper.Map<MonAnDTO>(monAn),
                    TotalVariantCount = monAn.ChiTietMonAns?.Count ?? 0,
                    MatchingVariantCount = 0, // Không cần tính cho khách hàng
                    MatchingVariantIds = new List<Guid>()
                }).ToList();

                // 12. Tạo response
                var response = new MonAnFilterResponse
                {
                    MonAns = monAnWithVariants,
                    TotalCount = totalCount,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalPages = totalPages
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi lọc sản phẩm: {ex.Message}" });
            }
        }

        /// <summary>
        /// DEBUG: Kiểm tra số lượng sản phẩm trong database
        /// </summary>
        [HttpGet("debug/count")]
        public async Task<ActionResult> DebugProductCount()
        {
            try
            {
                var minExpiryDate = DateTime.Now.AddDays(7);
                
                var totalProducts = await _context.monAns.CountAsync();
                var activeProducts = await _context.monAns.Where(m => m.TrangThai == true).CountAsync();
                var productsWithValidExpiry = await _context.monAns
                    .Where(m => m.TrangThai == true &&
                                m.ChiTietMonAns.Any(ct => 
                                    ct.TrangThai == true && 
                                    ct.Soluong > 0 && 
                                    ct.HanSuDung >= minExpiryDate))
                    .CountAsync();
                    
                var allChiTiet = await _context.chiTietMonAns.CountAsync();
                var validChiTiet = await _context.chiTietMonAns
                    .Where(ct => ct.TrangThai == true && ct.Soluong > 0 && ct.HanSuDung >= minExpiryDate)
                    .CountAsync();
                
                return Ok(new
                {
                    CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    MinExpiryDate = minExpiryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    TotalProducts = totalProducts,
                    ActiveProducts = activeProducts,
                    ProductsWithValidExpiry = productsWithValidExpiry,
                    TotalChiTiet = allChiTiet,
                    ValidChiTiet = validChiTiet,
                    Message = productsWithValidExpiry > 0 
                        ? $"✅ Có {productsWithValidExpiry} sản phẩm hợp lệ (HSD >= 7 ngày)" 
                        : "❌ KHÔNG có sản phẩm nào có HSD >= 7 ngày. Vui lòng update dữ liệu!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPost("filter")]
        public async Task<ActionResult<MonAnFilterResponse>> Filter([FromBody] MonAnFilterRequest filter)
        {
            // 1. Validate và thiết lập giá trị mặc định cho phân trang
            var pageIndex = filter.PageIndex ?? 1;
            var pageSize = filter.PageSize ?? 12; // THAY ĐỔI: PageSize mặc định = 12
            
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 12;

            // 2. Tạo query món ăn từ database (KHÔNG LOAD TOÀN BỘ)
            IQueryable<MonAn> monAnQuery = _context.monAns
                .Include(m => m.TheLoai)
                .Include(m => m.ThuongHieu)
                .Include(m => m.ChiTietMonAns)
                .ThenInclude(ct => ct.Anhs)
                .OrderByDescending(m => m.Id); // SẮP XẾP THEO MỚI NHẤT

            // 3. Query món ăn

            // 3.1 Filter theo Keyword (tìm kiếm)
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.ToLower().Trim();
                monAnQuery = monAnQuery.Where(m =>
                    (m.Ten != null && m.Ten.ToLower().Contains(keyword)) ||
                    (m.Id != null && m.Id.ToLower().Contains(keyword)) ||
                    (m.Mota != null && m.Mota.ToLower().Contains(keyword))
                );
            }

            // 3.2 Filter theo trạng thái món ăn
            if (filter.TrangThaiMonAn.HasValue)
            {
                monAnQuery = monAnQuery.Where(m => m.TrangThai == filter.TrangThaiMonAn.Value);
            }

            // 3.3 Filter theo thể loại
            if (filter.TheLoaiId.HasValue)
            {
                monAnQuery = monAnQuery.Where(m => m.TheLoaiId == filter.TheLoaiId.Value);
            }

            // 3.4 Filter theo thương hiệu
            if (filter.ThuongHieuId.HasValue)
            {
                monAnQuery = monAnQuery.Where(m => m.ThuongHieuId == filter.ThuongHieuId.Value);
            }

            // 4. Filter theo Chi tiết món ăn (logic phức tạp)
            var hasChiTietFilter = filter.HasChiTietFilter();
            
            if (hasChiTietFilter)
            {
                // Nếu có filter theo chi tiết, chỉ lấy món ăn có ít nhất 1 chi tiết thỏa điều kiện
                monAnQuery = monAnQuery.Where(m => m.ChiTietMonAns.Any(ct =>
                    // Filter theo trạng thái chi tiết
                    (!filter.TrangThaiChiTiet.HasValue || ct.TrangThai == filter.TrangThaiChiTiet.Value) &&
                    // Filter theo Loại vị
                    (!filter.LoaiViId.HasValue || ct.LoaiViId == filter.LoaiViId.Value) &&
                    // Filter theo Kích cỡ
                    (!filter.KichCoId.HasValue || ct.KichCoId == filter.KichCoId.Value) &&
                    // Filter theo Nhà cung cấp
                    (!filter.NhaCungCapId.HasValue || ct.NhaCungCapId == filter.NhaCungCapId.Value) &&
                    // Filter theo Nguyên liệu
                    (!filter.NguyenLieuId.HasValue || ct.NguyenLieuId == filter.NguyenLieuId.Value) &&
                    // Filter theo Giá
                    (!filter.GiaFrom.HasValue || ct.Gia >= filter.GiaFrom.Value) &&
                    (!filter.GiaTo.HasValue || ct.Gia <= filter.GiaTo.Value) &&
                    // Filter theo Số lượng
                    (!filter.SoLuongFrom.HasValue || ct.Soluong >= filter.SoLuongFrom.Value) &&
                    (!filter.SoLuongTo.HasValue || ct.Soluong <= filter.SoLuongTo.Value) &&
                    // Filter theo Ngày sản xuất
                    (!filter.NgaySanXuatFrom.HasValue || ct.NgaySanXuat >= filter.NgaySanXuatFrom.Value) &&
                    (!filter.NgaySanXuatTo.HasValue || ct.NgaySanXuat <= filter.NgaySanXuatTo.Value) &&
                    // Filter theo Hạn sử dụng
                    (!filter.HanSuDungFrom.HasValue || ct.HanSuDung >= filter.HanSuDungFrom.Value) &&
                    (!filter.HanSuDungTo.HasValue || ct.HanSuDung <= filter.HanSuDungTo.Value)
                ));
            }

            // 5. Đếm tổng số món ăn sau filter (TRƯỚC KHI PHÂN TRANG)
            var totalCount = await monAnQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // 6. PHÂN TRANG NGAY TẠI DATABASE (Skip/Take)
            var pagedMonAns = await monAnQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 7. Xử lý variant matching cho từng món ăn đã phân trang
            var monAnWithVariants = new List<MonAnWithMatchingVariants>();

            foreach (var monAn in pagedMonAns)
            {
                // Lấy chi tiết của món ăn này (đã được Include sẵn)
                var chiTiets = monAn.ChiTietMonAns?.ToList() ?? new List<ChiTietMonAn>();
                var totalVariantCount = chiTiets.Count;

                if (hasChiTietFilter)
                {
                    // Apply filter cho chi tiết để tính số lượng matching
                    var matchingChiTiets = chiTiets.Where(ct =>
                        (!filter.TrangThaiChiTiet.HasValue || ct.TrangThai == filter.TrangThaiChiTiet.Value) &&
                        (!filter.LoaiViId.HasValue || ct.LoaiViId == filter.LoaiViId.Value) &&
                        (!filter.KichCoId.HasValue || ct.KichCoId == filter.KichCoId.Value) &&
                        (!filter.NhaCungCapId.HasValue || ct.NhaCungCapId == filter.NhaCungCapId.Value) &&
                        (!filter.NguyenLieuId.HasValue || ct.NguyenLieuId == filter.NguyenLieuId.Value) &&
                        (!filter.GiaFrom.HasValue || ct.Gia >= filter.GiaFrom.Value) &&
                        (!filter.GiaTo.HasValue || ct.Gia <= filter.GiaTo.Value) &&
                        (!filter.SoLuongFrom.HasValue || ct.Soluong >= filter.SoLuongFrom.Value) &&
                        (!filter.SoLuongTo.HasValue || ct.Soluong <= filter.SoLuongTo.Value) &&
                        (!filter.NgaySanXuatFrom.HasValue || ct.NgaySanXuat >= filter.NgaySanXuatFrom.Value) &&
                        (!filter.NgaySanXuatTo.HasValue || ct.NgaySanXuat <= filter.NgaySanXuatTo.Value) &&
                        (!filter.HanSuDungFrom.HasValue || ct.HanSuDung >= filter.HanSuDungFrom.Value) &&
                        (!filter.HanSuDungTo.HasValue || ct.HanSuDung <= filter.HanSuDungTo.Value)
                    ).ToList();

                    var monAnDto = _mapper.Map<MonAnDTO>(monAn);
                    monAnWithVariants.Add(new MonAnWithMatchingVariants
                    {
                        MonAn = monAnDto,
                        MatchingVariantCount = matchingChiTiets.Count,
                        TotalVariantCount = totalVariantCount,
                        MatchingVariantIds = matchingChiTiets.Select(ct => ct.Id).ToList()
                    });
                }
                else
                {
                    // Không có filter theo chi tiết, hiển thị tất cả
                    var monAnDto = _mapper.Map<MonAnDTO>(monAn);
                    monAnWithVariants.Add(new MonAnWithMatchingVariants
                    {
                        MonAn = monAnDto,
                        MatchingVariantCount = 0, // Không tính khi không có filter
                        TotalVariantCount = totalVariantCount,
                        MatchingVariantIds = new List<Guid>()
                    });
                }
            }

            // 8. Tạo response
            var response = new MonAnFilterResponse
            {
                MonAns = monAnWithVariants,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return Ok(response);
        }
    }
}
