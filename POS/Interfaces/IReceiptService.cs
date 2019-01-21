using POS.Models;

namespace POS.Interfaces
{
    public interface IReceiptService
    {
        int Create();
        ReceiptItem Add(string ean);
        bool Remove(int receiptItemId);
        bool Delete(int receiptId);
        Receipt GetCurrentReceipt();
        PaymentInfo GetByReceiptId(int receiptId);
        ReceiptItem ChangeQuantity(int receiptItemId, decimal quantity);
    }
}
