using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.DataAccess;
using POS.Services;
using POS.Models.POS;
using Posnet;
using System;

namespace POS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private PosnetDriverPosnetProtocol posnet;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<PosDbContext>(options => options.UseSqlServer("Server=localhost;Database=POS;Trusted_Connection=True;"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            PosSettings settings = new PosSettings();
            Configuration.GetSection("POSSettings").Bind(settings);
            services.AddSingleton(settings);
            posnet = new PosnetDriverPosnetProtocol(new PosnetSettings
            {
                BaudRate = settings.FiscalPrinter.BaudRate,
                Handshake = settings.FiscalPrinter.Handshake,
                Port = settings.FiscalPrinter.Port

            });
            posnet.PrinterId = "TARGI";
            posnet.OperatorId = "TARGI";
            posnet.Open();
            services.AddSingleton(posnet);
            services.AddOpenApiDocument();

            // Register services
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IFiscalService, FiscalService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IReceiptService, ReceiptService>();

            // Register repos
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IFiscalGateway, FiscalGateway>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IReceiptRepository, ReceiptRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                DarkTheme = true,
                AutoHideMenuBar = true,
                Title = "POS",
                Fullscreen = true
            }));
            applicationLifetime.ApplicationStopping.Register(CloseConnection);
        }

        private void CloseConnection()
        {
            try
            {
                posnet.Logout();
                posnet.Close();
            }
            catch (Exception ea)
            {
            }
        }
    }
}
