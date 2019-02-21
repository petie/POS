using POS.Models;

namespace POS.Services
{
    public interface IReceiptService
    {
        Receipt Create();
        Receipt Add(string ean);
        Receipt Remove(int receiptId, int receiptItemId);
        Receipt Delete(int receiptId);
        Receipt GetCurrentReceipt();
        PaymentInfo GetByReceiptId(int receiptId);
        Receipt ChangeQuantity(int receiptItemId, decimal quantity);
    }
}
