namespace API.Models.DTO
{
    /// <summary>
    /// DTO duy nhất để gom toàn bộ điều kiện lọc và phân trang
    /// TẤT CẢ các field phải nullable để tránh lỗi binding/convert
    /// </summary>
    public class MonAnFilterRequest
    {
        // ===== PHÂN TRANG =====
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        // ===== TÌM KIẾM =====
        public string? Keyword { get; set; }

        // ===== LỌC THEO MÓN ĂN =====
        /// <summary>
        /// null = không lọc, true = hoạt động, false = tạm dừng
        /// </summary>
        public bool? TrangThaiMonAn { get; set; }
        
        public Guid? TheLoaiId { get; set; }
        public Guid? ThuongHieuId { get; set; }

        // ===== LỌC THEO CHI TIẾT MÓN ĂN =====
        /// <summary>
        /// null = không lọc, true = hoạt động, false = tạm dừng
        /// </summary>
        public bool? TrangThaiChiTiet { get; set; }

        public Guid? LoaiViId { get; set; }
        public Guid? KichCoId { get; set; }
        public Guid? NhaCungCapId { get; set; }
        public Guid? NguyenLieuId { get; set; }

        // Lọc theo giá
        public decimal? GiaFrom { get; set; }
        public decimal? GiaTo { get; set; }

        // Lọc theo số lượng
        public int? SoLuongFrom { get; set; }
        public int? SoLuongTo { get; set; }

        // Lọc theo ngày sản xuất
        public DateTime? NgaySanXuatFrom { get; set; }
        public DateTime? NgaySanXuatTo { get; set; }

        // Lọc theo hạn sử dụng
        public DateTime? HanSuDungFrom { get; set; }
        public DateTime? HanSuDungTo { get; set; }

        // ===== SẮP XẾP =====
        /// <summary>
        /// Sắp xếp: "newest" (mới nhất), "oldest" (cũ nhất), "price-asc" (giá tăng), "price-desc" (giá giảm)
        /// </summary>
        public string? SortBy { get; set; }

        // ===== HELPER =====
        /// <summary>
        /// Kiểm tra xem có điều kiện lọc theo chi tiết không
        /// </summary>
        public bool HasChiTietFilter()
        {
            return TrangThaiChiTiet.HasValue
                || LoaiViId.HasValue
                || KichCoId.HasValue
                || NhaCungCapId.HasValue
                || NguyenLieuId.HasValue
                || GiaFrom.HasValue
                || GiaTo.HasValue
                || SoLuongFrom.HasValue
                || SoLuongTo.HasValue
                || NgaySanXuatFrom.HasValue
                || NgaySanXuatTo.HasValue
                || HanSuDungFrom.HasValue
                || HanSuDungTo.HasValue;
        }
    }
}

