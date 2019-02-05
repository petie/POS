using POS.Models;

namespace POS.Services
{
    public interface IPaymentService
    {
        PaymentInfo Get(int receiptId);
        PaymentInfo Create();
        PaymentInfo PayAmount(PaymentPayload payment);
    }
}
