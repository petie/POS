using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class RemoveItemRequest
    {
        public RemoveItemRequest(int receiptId, int receiptItemId)
        {
            ReceiptId = receiptId;
            ReceiptItemId = receiptItemId;
        }
        public int ReceiptId { get; set; }
        public int ReceiptItemId { get; set; }

    }
}
