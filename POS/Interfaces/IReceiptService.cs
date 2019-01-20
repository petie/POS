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
        ActionResult<int> Create();
        ActionResult<bool> Add(ReceiptItem receiptItem);
        ActionResult<bool> Remove(int receiptItemId);
        ActionResult<bool> Delete(int receiptId);
    }
}
