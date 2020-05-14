using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopWebApp.Infrastructure;
using EshopWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EshopWebApp
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
            services.AddControllersWithViews();

            services.AddDbContext<EshopDbContext>(options =>
            {
                var conString = Configuration.GetConnectionString("PgSqlConnection");
                //options.UseInMemoryDatabase("eshopdb");
                options.UseNpgsql(conString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //InitializeDatabase(app);

            app.UseRouting(); //endpoint mapping 
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<EshopDbContext>();
                db.CatalogItems.Add(new CatalogItem { Name = "Galaxy S10", Price = 65000, Quantity = 10, Brand = "Samsung", Category = "Mobile", MfgDate = DateTime.Now });
                db.CatalogItems.Add(new CatalogItem { Name = "Android TV 50inch", Price = 45000, Quantity = 15, Brand = "Thomson", Category = "TV", MfgDate = DateTime.Now });
                db.SaveChanges();
            }
        }
        
    }
}
