
using POS.Services;
using POS.Models;
using POS.Models.POS;
using Posnet;
using System;
using System.IO.Ports;

namespace POS.DataAccess
{
    public class FiscalGateway : IFiscalGateway
    {
        //private readonly PosSettings settings;
        private PosnetDriverPosnetProtocol posnet;

        public FiscalGateway(PosnetDriverPosnetProtocol posnet)
        {
            this.posnet = posnet;
        }

        public void Login()
        {
            posnet.Login();
        }

        public void Print(Receipt receipt)
        {
            posnet.OpenReceipt(receipt.Id.ToString());
            foreach (var item in receipt.Items)
            {
                posnet.AddItem(item.ToReceiptItem());
            }
            posnet.AddPayment(receipt.Payment.ToPayment());
            posnet.Close();
        }
    }
}
