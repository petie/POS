using POS.Models;
using POS.Tests.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace POS.Tests.Integration
{
    public class ProductControllerIntegrationTests : BaseIntegrationTest
    {
        public ProductControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanGetProduct()
        {
            var httpResponse = await Client.GetAsync("/api/product/ean/5907771443270");

            httpResponse.EnsureSuccessStatusCode();

            var product = await httpResponse.GetResponse<Product>();
            Assert.Equal("5907771443270", product.Ean);
            Assert.True(product.Id != 0);
        }

        [Fact]
        public async Task CanGetAllProducts()
        {
            var httpResponse = await Client.GetAsync("/api/product");

            httpResponse.EnsureSuccessStatusCode();

            var product = await httpResponse.GetResponse<List<Product>>();
            Assert.Equal(3, product.Count);
        }
    }
}
