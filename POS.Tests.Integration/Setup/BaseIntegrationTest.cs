using System.Net.Http;
using Xunit;

namespace POS.Tests.Integration
{
    public class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        public HttpClient Client { get; set; }

        public BaseIntegrationTest(CustomWebApplicationFactory factory)
        {
            Client = factory.CreateClient();
        }
    }
}
