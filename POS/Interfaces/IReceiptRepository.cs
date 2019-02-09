using POS.Models;

namespace POS.Services
{
    public interface IReceiptRepository
    {
        Receipt FindByReceiptItemId(int receiptItemId);
        int Create(Receipt receipt);
        bool Save(Receipt receipt);
        Receipt Get(int receiptId);
        Receipt GetCurrent();
    }
}
