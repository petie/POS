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
            var shift = await Client.CreateShift();
            shift = await Client.StartShift(shift.Id, 100.0m);
            var receiptId = await Client.CreateReceipt();
            var receiptItem = await Client.AddToReceipt("5907771443270");


        }
    }
}
