using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Interfaces
{
    public interface IPaymentService
    {
        PaymentInfo Get(int receiptId);
    }
}
