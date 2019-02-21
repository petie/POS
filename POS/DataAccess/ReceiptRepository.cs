using Microsoft.EntityFrameworkCore;
using POS.Models;
using POS.Services;
using System.Linq;

namespace POS.DataAccess
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly PosDbContext context;

        public ReceiptRepository(PosDbContext context)
        {
            this.context = context;
        }

        public virtual Receipt Create(Receipt receipt)
        {
            context.Receipts.Add(receipt);
            context.SaveChanges();
            return receipt;
        }

        public virtual Receipt FindByReceiptItemId(int receiptItemId)
        {
            return context.ReceiptItems.Include(r => r.Receipt).SingleOrDefault(r => r.Id == receiptItemId)?.Receipt;
        }

        public virtual Receipt Get(int receiptId)
        {
            return context.Receipts
                .Include(r => r.AllItems)
                .ThenInclude(ri => ri.Product)
                .Include(r => r.Payment)
                .SingleOrDefault(r => r.Id == receiptId && !r.IsClosed);
        }

        public virtual Receipt GetCurrent()
        {
            return context.Receipts
                .Include(r => r.AllItems)
                .ThenInclude(ri => ri.Product)
                .Include(r => r.Payment)
                .SingleOrDefault(c => !c.IsClosed);
        }

        public bool Save(Receipt receipt)
        {
            context.Receipts.Update(receipt);
            context.SaveChanges();
            return true;
        }

        public bool Create(ReceiptItem receiptItem)
        {
            context.ReceiptItems.Add(receiptItem);
            context.SaveChanges();
            return true;
        }
    }
}
