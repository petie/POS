using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    public class NoActiveShiftException : Exception
    {
        public NoActiveShiftException() : base("Brak aktywnej zmiany, otwórz zmianę najpierw")
        {
        }
    }
}
