using System;
using JG.FinTechTest.Data;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JG.FinTechTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddTransient<IRepository, Repository>();
            services.TryAddTransient<IStoreTaxRate, TaxRateStorage>();
            services.TryAddTransient<GiftAidCalculator>();

            services.AddScoped<GiftAidDbContext>();
            services.AddScoped(x => new DbContextOptionsBuilder<DbContext>()
                //.UseSqlite(@"Data Source=.\GiftAid.db")
                .UseInMemoryDatabase("GiftAid")
                .Options);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
