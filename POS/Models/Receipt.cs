﻿using Newtonsoft.Json;
using POS.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace POS.Models
{
    public class Receipt
    {
        public Receipt()
        {
            Items = new List<ReceiptItem>();
        }

        public Receipt(Shift shift)
        {
            Shift = shift;
            ShiftId = shift.Id;
            Items = new List<ReceiptItem>();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shift"></param>
        public Receipt(int id, Shift shift) : this(shift)
        {
            Id = id;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<ReceiptItem> Items { get; set; }

        internal int GetNextOrdinalNumber() => 1 + Items?.Count ?? 0;

        public PaymentInfo Payment { get; set; }
        public int ShiftId { get; set; }
        [JsonIgnore]
        public Shift Shift { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsClosed { get; set; }
        public decimal Total => Items.Where(i => !i.IsRemoved).Sum(i => i.Value);
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        internal ReceiptItem AddItem(Product product)
        {
            ReceiptItem result = new ReceiptItem(product, this);
            Items.Add(result);

            DateModified = DateTime.Now;
            return result;
        }

        internal void Close()
        {
            IsClosed = true;
            DateModified = DateTime.Now;
        }

        internal ReceiptItem ChangeQuantity(int receiptItemId, decimal quantity)
        {
            ReceiptItem item = Items.SingleOrDefault(i => i.Id == receiptItemId);
            if (item == null)
                throw new ReceiptItemDoesNotExistException(receiptItemId);
            item.Quantity = quantity;
            return item;
        }

        internal bool RemoveItem(int receiptItemId)
        {
            if (!IsClosed && !IsCancelled)
            {
                var item = Items.SingleOrDefault(c => c.Id == receiptItemId);
                item.IsRemoved = true;
                item.OrdinalNumber = -1;
                ReorderActive();
                DateModified = DateTime.Now;
                return true;
            }
            return false;
        }

        private void ReorderActive()
        {
            var itemsToReorder = Items.Where(c => !c.IsRemoved).ToList();
            for (int i = 0; i < itemsToReorder.Count(); i++)
            {
                itemsToReorder[i].OrdinalNumber = i + 1;
            }
        }

        internal bool Cancel()
        {
            if (!IsClosed)
            {
                IsCancelled = true;
                IsClosed = true;
                DateModified = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
