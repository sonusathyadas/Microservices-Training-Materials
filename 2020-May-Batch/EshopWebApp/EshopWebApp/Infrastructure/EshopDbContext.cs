using EshopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopWebApp.Infrastructure
{
    public class EshopDbContext:DbContext
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CatalogItem>()
            //    .HasData(
            //    new CatalogItem {Id=1, Name = "Galaxy S10", Price = 65000, Quantity = 10, Brand = "Samsung", Category = "Mobile", MfgDate = DateTime.Now },
            //    new CatalogItem {Id=2, Name = "Android TV 50inch", Price = 45000, Quantity = 15, Brand = "Thomson", Category = "TV", MfgDate = DateTime.Now }
            //    );

        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
    }
}
