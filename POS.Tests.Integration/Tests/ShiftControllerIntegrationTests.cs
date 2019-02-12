using System.Net.Http;
using System.Threading.Tasks;
using POS.Models;
using Xunit;

namespace POS.Tests.Integration
{
    public class ShiftControllerIntegrationTests : BaseIntegrationTest
    {
        public ShiftControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanCloseShift()
        {
            var shift = await Client.CreateShift();
            Assert.True(shift.Id != 0);
            shift = await Client.StartShift(shift.Id, 100);
            Assert.True(shift.IsOpen && !shift.IsClosed);
            shift = await Client.CloseShift();
            Assert.True(shift.IsClosed);
        }
    }
}
