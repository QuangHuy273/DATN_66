using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class NhanVien
    {
        [Key]
        public string Id { get; set; }
        public bool TrangThai { get; set; } = false;
        public DateTime NgayVaoLam { get; set; }
        public Guid? ChucVuId { get; set; }
        public Guid NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
        public virtual ChucVu? ChucVu { get; set; }
    }
}
