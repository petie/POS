using POS.Services;
using POS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.DataAccess
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly PosDbContext context;

        public ReceiptRepository(PosDbContext context)
        {
            this.context = context;
        }

        public virtual int Create(Receipt receipt)
        {
            context.Receipts.Add(receipt);
            context.SaveChanges();
            return receipt.Id;
        }

        public virtual Receipt FindByReceiptItemId(int receiptItemId)
        {
            return context.ReceiptItems.Include(r => r.Receipt).SingleOrDefault(r => r.Id == receiptItemId)?.Receipt;
        }

        public virtual Receipt Get(int receiptId)
        {
            return context.Receipts.Include(r => r.Items).SingleOrDefault(r => r.Id == receiptId);
        }

        public virtual Receipt GetCurrent()
        {
            return context.Receipts.Include(r => r.Items).SingleOrDefault(c => !c.IsClosed);
        }

        public bool Save(Receipt receipt)
        {
            context.Receipts.Update(receipt);
            context.SaveChanges();
            return true;
        }
    }
}
