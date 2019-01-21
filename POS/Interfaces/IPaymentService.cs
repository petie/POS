using POS.Models;

namespace POS.Interfaces
{
    public interface IPaymentService
    {
        PaymentInfo Get(int receiptId);
        PaymentInfo Create();
        PaymentInfo PayAmount(PaymentPayload payment);
    }
}
