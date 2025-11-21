using System.ComponentModel.DataAnnotations;

namespace JollyWeb.ViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Tên đăng nhập không được chứa ký tự đặc biệt")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^(0[0-9]{9}|(\+84)[0-9]{9})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SDT { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu nhập lại không khớp")]
        public string RePassword { get; set; } = string.Empty;
    }
}
