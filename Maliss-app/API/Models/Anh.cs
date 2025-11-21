using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Anh
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Ten { get; set; }
        public string DuongDan { get; set; }
        public bool TrangThai { get; set; } = true;
        public Guid? NguoiDungId { get; set; }
        public Guid? ChiTietMonAnId { get; set; }


        public virtual ChiTietMonAn? ChiTietMonAn { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
