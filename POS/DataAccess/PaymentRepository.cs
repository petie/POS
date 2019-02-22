using POS.Services;
using POS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return context.Payments
                .Include(p => p.Receipt)
                .ThenInclude(r => r.AllItems)
                .ThenInclude(ri => ri.Product)
                .ThenInclude(p => p.Tax)
                .SingleOrDefault(c => !c.IsPayed);
        }

        public void Save(PaymentInfo info)
        {
            context.Payments.Add(info);
            context.SaveChanges();
        }

        public void Update(PaymentInfo info)
        {
            context.Payments.Update(info);
            context.SaveChanges();
        }
    }
}
