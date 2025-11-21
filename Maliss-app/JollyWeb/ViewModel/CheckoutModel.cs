using System.ComponentModel.DataAnnotations;

namespace JollyWeb.ViewModel
{
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Tỉnh/thành phố là bắt buộc")]
        public string Province { get; set; } = "";

        [Required(ErrorMessage = "Quận/huyện là bắt buộc")]
        public string District { get; set; } = "";

        [Required(ErrorMessage = "Xã/thị trấn là bắt buộc")]
        public string Ward { get; set; } = "";

        [Required(ErrorMessage = "Địa chỉ cụ thể là bắt buộc")]
        public string SpecificAddress { get; set; } = "";

        public string Notes { get; set; } = "";

        [Required]
        public string PaymentMethod { get; set; } = "cod";
    }
}
