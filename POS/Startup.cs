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
        private IFiscalDriver posnet;

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
            services.AddOpenApiDocument();
            services.AddSingleton<IFiscalDriver, PosnetDriverPosnetProtocol>();
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddSingleton<IFiscalService, FiscalService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IReceiptService, ReceiptService>();

            // Register repos
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<IFiscalGateway, FiscalGateway>();
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
            app.UseCors(builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            applicationLifetime.ApplicationStopping.Register(() => CloseConnection(app.ApplicationServices));
            ElectronBootstrap();
        }

        public async void ElectronBootstrap()
        {
            var prefs = new BrowserWindowOptions
            {
                Width = 1152,
                Height = 864,
                Show = false,
                WebPreferences = new WebPreferences { WebSecurity = false }
            };
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(prefs);
            browserWindow.OnReadyToShow += () => browserWindow.Show();
        }

        private void CloseConnection(IServiceProvider applicationServices)
        {
            try
            {
                var fg = applicationServices.GetService<IFiscalGateway>();
                fg.LogOut();
            }
            catch (Exception ea)
            {
            }
        }
    }
}
