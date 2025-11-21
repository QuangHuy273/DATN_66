namespace JollyWeb.Service.IService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(decimal amount, string orderId, string orderDesc, string bankCode = null, string locale = "vn");
    }
}
