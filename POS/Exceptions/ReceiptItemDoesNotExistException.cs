using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    public class ReceiptItemDoesNotExistException: Exception
    {
        public ReceiptItemDoesNotExistException(int receiptItemId): base("Nie istnieje taka pozycja paragonu")
        {
            ReceiptItemId = receiptItemId;
        }

        public int ReceiptItemId { get; }
    }
}
