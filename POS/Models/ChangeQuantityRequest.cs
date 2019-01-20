using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class ChangeQuantityRequest
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
    }
}
