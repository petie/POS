using POS.Models;
using POS.Models.POS;
using Posnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.DataAccess
{
    public static class Extensions
    {
        public static PosnetReceiptItem ToReceiptItem(this ReceiptItem r)
        {
            return new PosnetReceiptItem(r.Product.Name, (long)(r.Quantity * 100), 2, r.Product.Unit, (long)(r.Product.Price * 100), r.Product.Tax.ToVatRate());
        }

        public static VatRate ToVatRate(this Tax t)
        {
            return Enum.Parse<VatRate>(t.FiscalSymbol);
        }

        public static PosnetPayment ToPayment(this PaymentInfo p)
        {
            PosnetPayment result = new PosnetPayment
            {
                Name = "gotówka",
                Type = 1,
                Value = (long)(p.AmountPayed * 100)
            };
            return result;
        }
    }
}
