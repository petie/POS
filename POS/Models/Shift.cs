using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Shift
    {
        public int Id { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal StartMoney { get; internal set; }
        public decimal StartDeposit { get; private set; }
        public decimal NumberOfReceipts => Receipts.Count(r => !r.IsCancelled);
        public bool IsClosed { get { return EndDate != null; } }
        public bool IsOpen { get { return StartDate != null && !IsClosed; } }
        public int CancelledReceiptsCount => Receipts.Count(r => r.IsCancelled);
        public int RemovedItemsCount => Receipts.Sum(r => r.Items.Count(i => i.IsRemoved));
        public IEnumerable<Receipt> Receipts { get; private set; }
        public decimal EndMoney => StartMoney + StartDeposit + Receipts.Sum(r => r.Total);

        public Shift(decimal startMoney)
        {
            StartDate = DateTime.Now;
            StartMoney = startMoney;
        }

        internal void Close()
        {
            EndDate = DateTime.Now;
        }

        internal void Start(decimal paymentAmount)
        {
            StartDeposit = paymentAmount;
            StartDate = DateTime.Now;

        }
    }
}
