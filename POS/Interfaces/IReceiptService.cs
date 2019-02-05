using POS.Models;

namespace POS.Services
{
    public interface IReceiptService
    {
        int Create();
        ReceiptItem Add(string ean);
        bool Remove(int receiptId, int receiptItemId);
        bool Delete(int receiptId);
        Receipt GetCurrentReceipt();
        PaymentInfo GetByReceiptId(int receiptId);
        ReceiptItem ChangeQuantity(int receiptItemId, decimal quantity);
    }
}
