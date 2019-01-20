using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public IEnumerable<ReceiptItem> Items { get; set; }
        public PaymentInfo Payment { get; set; }
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        public bool IsCancelled { get; set; }
        public decimal Total { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
