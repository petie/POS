﻿using POS.Models;

namespace POS.Services
{
    public interface IReceiptRepository
    {
        Receipt FindByReceiptItemId(int receiptItemId);
        Receipt Create(Receipt receipt);
        bool Save(Receipt receipt);
        bool Create(ReceiptItem receiptItem);
        Receipt Get(int receiptId);
        Receipt GetCurrent();
    }
}
