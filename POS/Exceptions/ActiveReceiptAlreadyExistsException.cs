using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    public class ActiveReceiptAlreadyExistsException : Exception
    {
        public ActiveReceiptAlreadyExistsException(int activeReceiptId) : base("Istnieje aktywny paragon. Zamknij najpierw ten paragon")
        {
            ActiveReceiptId = activeReceiptId;
        }

        public int ActiveReceiptId { get; }
    }
}
