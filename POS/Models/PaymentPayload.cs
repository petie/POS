using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class PaymentPayload
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
    }
}
