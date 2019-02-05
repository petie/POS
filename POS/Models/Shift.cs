using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace POS.Models
{
    public class Shift
    {
        public Shift()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal StartMoney { get; internal set; }
        public decimal StartDeposit { get; private set; }
        public decimal NumberOfReceipts => Receipts?.Count(r => !r.IsCancelled) ?? 0;
        public bool IsClosed { get { return EndDate != null; } }
        public bool IsOpen { get { return StartDate != null && !IsClosed; } }
        public int CancelledReceiptsCount => Receipts?.Count(r => r.IsCancelled) ?? 0;
        public int RemovedItemsCount => Receipts?.Sum(r => r.Items.Count(i => i.IsRemoved)) ?? 0;
        public IEnumerable<Receipt> Receipts { get; private set; }
        public decimal EndMoney => StartMoney + StartDeposit + Receipts?.Sum(r => r.Total) ?? 0;

        public Shift(decimal startMoney)
        {
            StartMoney = startMoney;
        }

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startMoney"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public Shift(int id, decimal startMoney, DateTime? startDate = null, DateTime? endDate = null) : this(startMoney)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
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
