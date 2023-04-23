using Autofac;
using Autofac.Extensions.DependencyInjection;
using FlightSearchApp.Application;
using FlightSearchApp.Domain;
using FlightSearchApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlightSearchApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICodeListService, CodeListService>();
            services.AddScoped<ICodeListRepository, CodeListRepository>();
            services.AddScoped<IFlightOfferService, FlightOfferService>();
            services.AddScoped<IFlightOfferRepository, FlightOfferRepisitory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}");
            });
        }
    }
}
