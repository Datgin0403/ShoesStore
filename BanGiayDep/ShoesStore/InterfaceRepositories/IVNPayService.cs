using ShoesStore.Models.Vnpay;

namespace ShoesStore.InterfaceRepositories
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
