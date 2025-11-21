using System.ComponentModel.DataAnnotations;

namespace JollyWeb.ViewModel
{
    public class RegisterThongtin
    {
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string Ho { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Ten { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        [MinAge(16, 70)]
        public DateTime NgaySinh { get; set; }   

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string? GioiTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Gmail { get; set; }


    }
}
