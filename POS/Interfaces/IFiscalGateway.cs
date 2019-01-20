using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;

namespace POS.Interfaces
{
    public interface IFiscalGateway
    {
        void Print(Receipt receipt);
    }
}
