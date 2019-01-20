using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal StartMoney { get; set; }
        public decimal StartDeposit { get; set; }
        public decimal NumberOfReceipts { get; set; }
        public bool IsClosed { get; set; }
        public int CancelledReceiptsCount { get; set; }
        public int RemovedItemsCount { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
    }
}
