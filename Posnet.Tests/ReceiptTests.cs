using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;
using POS.Controllers;
using POS.DataAccess;
using POS.Models;
using POS.Services;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Posnet.Tests
{
    [TestClass]
    public class ReceiptTests
    {
        private PosnetSettings _settings = new PosnetSettings
        {
            BaudRate = 9600,
            Handshake = Handshake.None,
            Name = "Posnet",
            Port = "COM7"
        };
        private List<Shift> shifts;
        private List<Receipt> receipts;
        private List<ReceiptItem> receiptItems;
        private List<Product> products;
        private Mock<PosDbContext> mockContext;

        [ClassInitialize]
        public void InitializeDbSets()
        {
            shifts = new List<Shift>
            {
                new Shift(1, 100, DateTime.Now, DateTime.Now),
                new Shift(2, 100, DateTime.Now)
            };
            receipts = new List<Receipt>
            {
                new Receipt(shifts[0])
            };
            products = new List<Product>
            {
                new Product(1, "1", 1, "szt", 2),
                new Product(2, "2", 2, "szt", 3),
                new Product(3, "3", 3, "szt", 4)
            };
            receiptItems = new List<ReceiptItem>
            {
                new ReceiptItem(1, products[0], receipts[0]),
                new ReceiptItem(2, products[1], receipts[0])
            };
            mockContext = new Mock<PosDbContext>();
            mockContext.Setup(x => x.Receipts).ReturnsDbSet(receipts);
            mockContext.Setup(x => x.ReceiptItems).ReturnsDbSet(receiptItems);
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            mockContext.Setup(x => x.Shifts).ReturnsDbSet(shifts);
        }

        [TestMethod]
        public void CreateReceipt()
        {
            PosnetDriverPosnetProtocol posnet = new PosnetDriverPosnetProtocol(_settings);
            posnet.OperatorId = "TEST";
            posnet.Open();
            posnet.Login();
            posnet.OpenReceipt("1");
            posnet.AddItem(new PosnetReceiptItem("test", 100, 2, 123, VatRate.A));
            posnet.CloseReceipt();
        }

        [TestMethod]
        public void TestSetVat()
        {
            PosnetDriverPosnetProtocol posnet = new PosnetDriverPosnetProtocol(_settings);
            posnet.OperatorId = "TEST";
            posnet.Open();
            posnet.SaveTaxRates(new List<KeyValuePair<VatRate, int>>()
            {
                new KeyValuePair<VatRate, int>(VatRate.A, 2300),
                new KeyValuePair<VatRate, int>(VatRate.D, 500),
                new KeyValuePair<VatRate, int>(VatRate.B, 800),
                new KeyValuePair<VatRate, int>(VatRate.C, 0),
            });
            posnet.Close();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        [TestMethod]
        public void AddItemToReceiptTest()
        {

        }

        [TestMethod]
        public void AddNullToReceiptTest()
        {

        }

        [TestMethod]
        public void AddItemToNotExistingReceiptTest()
        {

        }

        [TestMethod]
        public void CreateReceiptTest()
        {

        }

        [TestMethod]
        public void CreateReceiptWithoutShiftTest()
        {

        }

        [TestMethod]
        public void CloseReceiptTest()
        {

        }

        [TestMethod]
        public void CloseReceiptNotOpenedTest()
        {

        }

        [TestMethod]
        public void ChangeQuantityOnItemTest()
        {
            var controller = CreateController();
        }

        private ReceiptController CreateController()
        {
            return new ReceiptController(
                new ReceiptService(
                    new ReceiptRepository(
                        mockContext.Object, new ShiftRepository(mockContext.Object)
                    ),
                    new ProductService(
                        new ProductRepository(mockContext.Object)
                    )
                )
            );
        }
    }
}
