using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPayed { get; set; }
        public decimal Change { get; set; }
        public bool IsPayed { get; set; }
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
