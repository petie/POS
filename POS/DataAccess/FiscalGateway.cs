
using POS.Services;
using POS.Models;
using POS.Models.POS;
using Posnet;
using System;
using System.IO.Ports;
using System.Diagnostics;

namespace POS.DataAccess
{
    public class FiscalGateway : IFiscalGateway
    {
        //private readonly PosSettings settings;
        private IFiscalDriver posnet;

        public FiscalGateway(IFiscalDriver posnet, PosSettings settings)
        {
            this.posnet = posnet;
            posnet.Setup(settings?.FiscalPrinter?.GetPosnetSettings());
            posnet.OperatorId = "Targi";
            posnet.Open();
            Debug.WriteLine("Opened connection");
            Login();

        }

        public void LogOut()
        {
            posnet.Logout();
            posnet.Close();
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
