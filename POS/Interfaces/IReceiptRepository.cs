using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
