using POS.Interfaces;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
