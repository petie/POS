using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class PaymentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPayed { get; set; }
        public decimal Change { get; set; }
        public bool IsPayed { get; set; }
        [JsonIgnore]
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public PaymentInfo(Receipt receipt)
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Receipt = receipt;
            ReceiptId = receipt.Id;
            Amount = receipt.Total;
        }

        public PaymentInfo()
        {

        }

        internal bool Pay(PaymentPayload payment)
        {
            if (payment.PaymentId == Id && payment.Amount >= Amount)
            {
                AmountPayed = payment.Amount;
                Change = payment.Amount - Amount;
                IsPayed = true;
                Receipt.Close();
                return true;
            }
            return false;
        }
    }
}
