using POS.Models;

namespace POS.Interfaces
{
    public interface IReceiptRepository
    {
        Receipt FindByReceiptItemId(int receiptItemId);
        int Create();
        bool Save(Receipt receipt);
        Receipt Get(int receiptId);
        Receipt GetCurrent();
    }
}
