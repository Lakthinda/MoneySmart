﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneySmart.API.Entities;
using MoneySmart.API.Models;
using MoneySmart.API.Services;
using System.Linq;

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

            services.AddScoped<ISavingsRepository,SavingsRepository>();            
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

            //Automapper Config
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<SavingAccount, SavingAccountDto>()
                //   .ForMember(dest => dest.TotalSavings,
                //              src => src.MapFrom(new SavingsAccountCustomResolver())
                //             );

                cfg.CreateMap<SavingAccount, SavingAccountDto>()
                        .ForMember(dest => dest.TotalSavings,
                            s => s.MapFrom(source => source.Transactions.Sum(t => t.Amount)));
                cfg.CreateMap<SavingAccount, SavingAccountWithoutTransactionsDto>()
                        .ForMember(dest => dest.TotalSavings,
                            s => s.MapFrom(source => source.Transactions.Sum(t => t.Amount)));
                cfg.CreateMap<Transaction, TransactionDto>();
            });

            app.UseStatusCodePages();

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Welcome to MoneySmart App..!");
            });
        }
    }

    #region AutoMapper Custom value Resolver
    public interface IValueResolver<in TSource, in TDestination, TDestMember>
    {
        TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }

    public class SavingsAccountCustomResolver : IValueResolver<SavingAccount, object, double>
    {
        public double Resolve(SavingAccount source, object destination, double destMember, ResolutionContext context)
        {
            return source.Transactions.Sum(t => t.Amount);
        }
    }

    #endregion
}
