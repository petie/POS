using POS.Interfaces;
using POS.Models;
using System.Linq;

namespace POS.DataAccess
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PosDbContext context;

        public PaymentRepository(PosDbContext context)
        {
            this.context = context;
        }

        public PaymentInfo GetCurrent()
        {
            return context.Payments.SingleOrDefault(c => !c.IsPayed);
        }

        public void Save(PaymentInfo info)
        {
            context.Payments.Add(info);
            context.SaveChanges();
        }
    }
}
