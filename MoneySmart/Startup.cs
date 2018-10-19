using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneySmart.API.Entities;
using MoneySmart.API.Services;

namespace MoneySmart
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                           .AddJsonFile($"appSettings.{env.EnvironmentName}", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static IConfiguration Configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(o => o.OutputFormatters.Add(
                            new XmlDataContractSerializerOutputFormatter()));

            var connectionString = Startup.Configuration["connectionStrings:moneySmartConnectionString"];
            services.AddDbContext<MoneySmartDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<SavingsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
