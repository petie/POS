using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;
using POS.Controllers;
using POS.DataAccess;
using POS.Exceptions;
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

        private Mock<IProductService> mockProductService;
        private Mock<IReceiptRepository> mockRepository;
        private Mock<IShiftService> mockShiftService;

        [TestInitialize]
        public void InitializeMocks()
        {
            mockProductService = new Mock<IProductService>();
            mockRepository = new Mock<IReceiptRepository>();
            mockShiftService = new Mock<IShiftService>();
        }

        //[TestMethod]
        //public void CreateReceipt()
        //{
        //    PosnetDriverPosnetProtocol posnet = new PosnetDriverPosnetProtocol(_settings);
        //    posnet.OperatorId = "TEST";
        //    posnet.Open();
        //    posnet.Login();
        //    posnet.OpenReceipt("1");
        //    posnet.AddItem(new PosnetReceiptItem("test", 100, 2, 123, VatRate.A));
        //    posnet.CloseReceipt();
        //}

        //[TestMethod]
        //public void TestSetVat()
        //{
        //    PosnetDriverPosnetProtocol posnet = new PosnetDriverPosnetProtocol(_settings);
        //    posnet.OperatorId = "TEST";
        //    posnet.Open();
        //    posnet.SaveTaxRates(new List<KeyValuePair<VatRate, int>>()
        //    {
        //        new KeyValuePair<VatRate, int>(VatRate.A, 2300),
        //        new KeyValuePair<VatRate, int>(VatRate.D, 500),
        //        new KeyValuePair<VatRate, int>(VatRate.B, 800),
        //        new KeyValuePair<VatRate, int>(VatRate.C, 0),
        //    });
        //    posnet.Close();
        //}

        //private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Console.WriteLine(e.ToString());
        //}

        [TestMethod]
        public void AddItemToReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt());
            mockProductService.Setup(s => s.Get("123")).Returns(new Product(1, "123", 1, "szt", 1));
            var service = CreateService();
            var result = service.Add("123");
            Assert.AreEqual("123", result.Product.Ean);
            Assert.AreEqual(1, result.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullToReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt());
            //mockProductService.Setup(s => s.Get(null)).Returns<Product>(null);
            var service = CreateService();
            var result = service.Add(null);
            //Assert.AreEqual("123", result.Product.Ean);
            //Assert.AreEqual(1, result.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(NoActiveReceiptException))]
        public void AddItemToNotExistingReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns<Receipt>(null);
            mockProductService.Setup(s => s.Get("123")).Returns(new Product(1, "123", 1, "szt", 1));
            var service = CreateService();
            var result = service.Add("123");
            //Assert.AreEqual("123", result.Product.Ean);
            //Assert.AreEqual(1, result.Quantity);
        }

        [TestMethod]
        public void CreateReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns<Receipt>(null);
            mockRepository.Setup(r => r.Create(It.IsAny<Receipt>())).Returns(1);
            var service = CreateService();
            var result = service.Create();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NoActiveShiftException))]
        public void CreateReceiptWithoutShiftTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns<Shift>(null);
            var service = CreateService();
            var result = service.Create();
        }

        [TestMethod]
        [ExpectedException(typeof(ActiveReceiptAlreadyExistsException))]
        public void CreateReceiptWithExistingReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt());
            //mockRepository.Setup(r => r.Create(It.IsAny<Receipt>())).Returns(1);
            var service = CreateService();
            var result = service.Create();
        }

        [TestMethod]
        public void ChangeQuantityOnItemTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt { Items = new List<ReceiptItem> { new ReceiptItem() { Id = 1} } });
            //mockRepository.Setup(r => r.Create(It.IsAny<Receipt>())).Returns(1);
            var service = CreateService();
            var result = service.ChangeQuantity(1, 10);
            Assert.AreEqual(10, result.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ReceiptItemDoesNotExistException))]
        public void ChangeQuantityMissingReceiptItemTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt { Items = new List<ReceiptItem> { new ReceiptItem() { Id = 2 } } });
            var service = CreateService();
            var result = service.ChangeQuantity(1, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NoActiveReceiptException))]
        public void ChangeQuantityNoReceiptTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns(new Shift(1, 100, DateTime.Now));
            mockRepository.Setup(r => r.GetCurrent()).Returns<Receipt>(null);
            var service = CreateService();
            var result = service.ChangeQuantity(1, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NoActiveShiftException))]
        public void ChangeQuantityNoShiftTest()
        {
            mockShiftService.Setup(s => s.GetCurrent()).Returns<Shift>(null);
            mockRepository.Setup(r => r.GetCurrent()).Returns(new Receipt { Items = new List<ReceiptItem> { new ReceiptItem() { Id = 1 } } });
            //mockRepository.Setup(r => r.Create(It.IsAny<Receipt>())).Returns(1);
            var service = CreateService();
            var result = service.ChangeQuantity(1, 10);
            Assert.AreEqual(10, result.Quantity);
        }

        private ReceiptService CreateService()
        {
            return new ReceiptService(mockRepository.Object, mockProductService.Object, mockShiftService.Object);
        }
    }
}
