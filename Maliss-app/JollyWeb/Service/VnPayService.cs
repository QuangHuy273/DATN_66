using JollyWeb.Service.IService;
using VNPAY_CS_ASPX;

namespace JollyWeb.Service
{
    public class VnPayService : IVnPayService
    {
        private const string tmnCode = "E3QQUILQ";
        private const string hashSecret = "R5VLRHTQY4PXHRKKR530F36FVEV4JS19";
        private const string baseUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private const string returnUrl = "https://localhost:7084/payment-result";

        private readonly IConfiguration _config;

        public VnPayService(IConfiguration config)
        {
            _config = config;
        }

        public string CreatePaymentUrl(decimal amount, string orderId, string orderDesc, string bankCode = null, string locale = "vn")
        {


            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(returnUrl) || string.IsNullOrEmpty(tmnCode) || string.IsNullOrEmpty(hashSecret))
            {
                throw new Exception("Thiếu config VNPAY trong appsettings.json");
            }

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", tmnCode);
            vnpay.AddRequestData("vnp_Amount", ((int)(amount * 100)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_Locale", locale ?? "vn");
            vnpay.AddRequestData("vnp_OrderInfo", orderDesc);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnpay.AddRequestData("vnp_TxnRef", orderId);

            if (!string.IsNullOrEmpty(bankCode))
            {
                vnpay.AddRequestData("vnp_BankCode", bankCode);
            }

            string paymentUrl = vnpay.CreateRequestUrl(baseUrl, hashSecret);
            return paymentUrl;
        }

    }
}
