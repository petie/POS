using Microsoft.VisualStudio.TestTools.UnitTesting;
using Posnet;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Posnet.Tests
{
    [TestClass]
    public class ReceiptTests
    {
        [TestMethod]
        public void CreateReceipt()
        {
            PosnetDriverPosnetProtocol posnet = new PosnetDriverPosnetProtocol(new PosnetSettings
            {
                BaudRate = 19200,
                Handshake = System.IO.Ports.Handshake.None,
                Name = "Posnet",
                Port = "COM8"
            });
            posnet.Open();
            posnet.OpenReceipt("1");
            posnet.AddItem(new PosnetReceiptItem("test", 1, 2, 100, VatRate.A));
            posnet.CloseReceipt();
        }

        [TestMethod]
        public async Task TestSerialPort()
        {
            SerialPort port = new SerialPort("COM8", 19200,Parity.Even);
            port.WriteTimeout = 5000;
            port.ReadTimeout = 5000;
            try
            {
                port.Open();
                //await port.WriteLineAsync("Hello World");
                var result = await port.SendCommandAsync("Hello World");
                port.Close();
            } catch (Exception ea)
            {
                Console.WriteLine(ea);
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
