﻿using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace POS.Tests.Integration.Tests
{
    public class ReceiptControllerIntegrationTest : BaseIntegrationTest
    {
        public ReceiptControllerIntegrationTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanCreateReceipt()
        {
            var ean1 = "5907771443270";
            var price1 = 4.99m;
            var ean2 = "5907814660503";
            var price2 = 6.70m;
            var shift = await Client.CreateShift();
            shift = await Client.StartShift(shift.Id, 100.0m);

            var receiptId = await Client.CreateReceipt();
            Assert.True(receiptId > 0);

            var receiptItem = await Client.AddToReceipt(ean1);
            Assert.True(!string.IsNullOrEmpty(receiptItem?.Ean) && receiptItem.Ean == ean1);
            Assert.Equal(1m, receiptItem.Quantity, 2);
            Assert.Equal(1, receiptItem.OrdinalNumber);
            Assert.Equal(price1, receiptItem.Value);
            Assert.Equal(1, receiptItem.OrdinalNumber);

            var receiptItem2 = await Client.AddToReceipt(ean2);
            Assert.True(!string.IsNullOrEmpty(receiptItem2?.Ean) && receiptItem2.Ean == ean2);
            Assert.Equal(1m, receiptItem2.Quantity, 2);
            Assert.Equal(2, receiptItem2.OrdinalNumber);
            Assert.Equal(price2, receiptItem2.Value);
            Assert.Equal(2, receiptItem2.OrdinalNumber);

            var changeQuantity = new ChangeQuantityRequest(receiptItem.Id, 2m);
            var receiptItem3 = await Client.ChangeQuantity(changeQuantity);
            Assert.Equal(2m, receiptItem3.Quantity);
            Assert.Equal(1, receiptItem3.OrdinalNumber);
            Assert.Equal(9.98m, receiptItem3.Value);
            Assert.Equal(receiptItem.Id, receiptItem3.Id);

            var isRemoved = await Client.RemoveItemFromReceipt(receiptId, receiptItem.Id);
            var receipt = await Client.GetCurrentReceipt();
            Assert.True(isRemoved);
            Assert.Single(receipt.Items);
            Assert.Equal(1, receipt.Items[0].OrdinalNumber);
            Assert.Equal(ean2, receipt.Items[0].Ean);
        }
    }
}
