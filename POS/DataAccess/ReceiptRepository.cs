using POS.Services;
using POS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return context.ReceiptItems.Include(r => r.Receipt).SingleOrDefault(r => r.Id == receiptItemId)?.Receipt;
        }

        public Receipt Get(int receiptId)
        {
            return context.Receipts.Include(r => r.Items).SingleOrDefault(r => r.Id == receiptId);
        }

        public Receipt GetCurrent()
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
