using POS.Models;

namespace POS.Interfaces
{
    public interface IPaymentRepository
    {
        void Save(PaymentInfo info);
        PaymentInfo GetCurrent();
    }
}
