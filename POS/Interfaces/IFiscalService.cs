using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;

namespace POS.Interfaces
{
    public interface IFiscalService
    {
        void Print(Receipt receipt);
    }
}
