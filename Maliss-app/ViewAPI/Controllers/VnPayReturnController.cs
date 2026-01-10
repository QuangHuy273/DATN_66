using Microsoft.AspNetCore.Mvc;
using VNPAY_CS_ASPX;

namespace ViewAPI.Controllers
{
    [Route("/payment-result")]
    public class VnPayReturnController : Controller
    {
        private readonly IConfiguration _config;
        public VnPayReturnController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vnpay = new VnPayLibrary();
            var query = HttpContext.Request.Query;
            foreach (var (key, value) in query)
            {
                vnpay.AddResponseData(key, value);
            }

            string hashSecret = "R5VLRHTQY4PXHRKKR530F36FVEV4JS19";
            bool checkSignature = vnpay.ValidateSignature(query["vnp_SecureHash"], hashSecret);
            Console.WriteLine("HashSecret = " + hashSecret);

            if (checkSignature)
            {
                string rspCode = query["vnp_ResponseCode"];
                if (rspCode == "00")
                {
                    return Content("Thanh toán thành công!");
                }
                else
                {
                    return Content("Thanh toán thất bại, mã lỗi: " + rspCode);
                }
            }
            else
            {
                return Content("Chữ ký không hợp lệ.");
            }
        }
    }
}
