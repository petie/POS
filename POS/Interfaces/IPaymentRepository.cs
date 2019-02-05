using POS.Models;

namespace POS.Services
{
    public interface IPaymentRepository
    {
        void Save(PaymentInfo info);
        PaymentInfo GetCurrent();
    }
}
