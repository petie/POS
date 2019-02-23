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
            bool hasFractions = !r.Product.Unit.StartsWith("szt");
            long amount = hasFractions ? (long)(r.Quantity * 10000) : (long)(r.Quantity * 100);
            long price = (long)(r.Product.Price * 100);
            return new PosnetReceiptItem(r.Product.Name, amount, hasFractions ? 4 : 2, r.Product.Unit, price, r.Product.Tax.ToVatRate());
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
                Type = 0,
                Value = (long)(p.AmountPayed * 100)
            };
            return result;
        }
    }
}
