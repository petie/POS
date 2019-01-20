using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
