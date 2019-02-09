using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    public class NoActiveReceiptException : Exception
    {
        public NoActiveReceiptException() : base("Brak aktywnego paragonu")
        {

        }
    }
}
