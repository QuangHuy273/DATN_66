namespace API.Models.DTO
{
    public class AnhDTO
    {
        public Guid Id { get; set; }
        public string? Ten { get; set; }
        public string? DuongDan { get; set; }
        public bool? TrangThai { get; set; }
        public Guid? NguoiDungId { get; set; }
        public Guid? ChiTietMonAnId { get; set; }
        /// <summary>
        /// Liên kết trực tiếp tới món ăn (nếu ảnh thuộc về món ăn thay vì chi tiết món ăn)
        /// </summary>
        public string? MonAnId { get; set; }


    }
}
