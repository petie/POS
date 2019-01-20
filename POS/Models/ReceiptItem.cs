using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class ReceiptItem
    {
        public int Id { get; set; }
        public int OrdinalNumber { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public bool IsRemoved { get; set; }
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public decimal TaxValue { get; set; }
    }
}
