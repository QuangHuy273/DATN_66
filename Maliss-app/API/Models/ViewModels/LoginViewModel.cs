using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập tài khoản!")]
        public string? TaiKhoan { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage ="Vui lòng nhập mật khẩu!")]
        public string? MatKhau { get; set; } = string.Empty ;
    }
}
