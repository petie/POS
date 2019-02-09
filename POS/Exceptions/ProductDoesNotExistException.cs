using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException(string ean) : base("Nie ma produktu o podanym EAN-ie")
        {
            Ean = ean;
        }

        public string Ean { get; }
    }
}
