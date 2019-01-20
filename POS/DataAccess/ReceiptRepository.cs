using POS.Interfaces;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.DataAccess
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly PosDbContext context;
        private readonly IShiftRepository shiftRepository;

        public ReceiptRepository(PosDbContext context, IShiftRepository shiftRepository)
        {
            this.context = context;
            this.shiftRepository = shiftRepository;
        }

        public int Create()
        {
            Shift shift = shiftRepository.GetActive();
            Receipt receipt = new Receipt(shift);
            context.Receipts.Add(receipt);
            context.SaveChanges();
            return receipt.Id;
        }

        public Receipt FindByReceiptItemId(int receiptItemId)
        {
            return context.ReceiptItems.Find(receiptItemId).Receipt;
        }

        public Receipt Get(int receiptId)
        {
            return context.Receipts.Find(receiptId);
        }

        public Receipt GetCurrent()
        {
            return context.Receipts.SingleOrDefault(c => !c.IsClosed);
        }

        public bool Save(Receipt receipt)
        {
            context.Receipts.Update(receipt);
            context.SaveChanges();
            return true;
        }
    }
}
