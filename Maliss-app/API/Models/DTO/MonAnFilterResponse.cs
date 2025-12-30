namespace API.Models.DTO
{
    /// <summary>
    /// Response trả về kết quả filter + pagination
    /// </summary>
    public class MonAnFilterResponse
    {
        /// <summary>
        /// Danh sách món ăn sau khi filter và phân trang
        /// </summary>
        public List<MonAnWithMatchingVariants> MonAns { get; set; } = new();

        /// <summary>
        /// Tổng số món ăn thỏa điều kiện (dùng cho phân trang)
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Số món ăn mỗi trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }
    }

    /// <summary>
    /// Món ăn kèm theo số biến thể phù hợp
    /// </summary>
    public class MonAnWithMatchingVariants
    {
        public MonAnDTO MonAn { get; set; } = null!;

        /// <summary>
        /// Số biến thể phù hợp với điều kiện lọc
        /// Chỉ có giá trị > 0 khi có filter theo chi tiết
        /// </summary>
        public int MatchingVariantCount { get; set; }

        /// <summary>
        /// Tổng số biến thể của món ăn
        /// </summary>
        public int TotalVariantCount { get; set; }

        /// <summary>
        /// Danh sách ID các chi tiết thỏa điều kiện (dùng cho highlight)
        /// </summary>
        public List<Guid> MatchingVariantIds { get; set; } = new();
    }
}

