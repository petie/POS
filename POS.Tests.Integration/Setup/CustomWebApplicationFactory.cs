using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POS.DataAccess;
using POS.Services;
using POS.Tests.Integration;
using POS.Tests.Integration.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<PosDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryPosDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<PosDbContext>();
                    appDb.Database.EnsureCreated();
                    SeedData.PopulateTestData(appDb);
                }
            });
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IFiscalGateway, MockFiscalGateway>();
            });

            base.ConfigureWebHost(builder);
        }
    }
}
