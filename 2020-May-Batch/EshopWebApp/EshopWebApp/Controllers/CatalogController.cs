using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopWebApp.Infrastructure;
using EshopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApp.Controllers
{
    [Route("products")]
    public class CatalogController : Controller
    {
        private EshopDbContext db;

        public CatalogController(EshopDbContext dbContext)
        {
            this.db = dbContext;
        }

        [Route("list", Name ="ListProducts")]
        public async Task<IActionResult> IndexAsync()
        {
            var products = await db.CatalogItems.ToListAsync();
            return View(products);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CatalogItem item)
        {
            if(ModelState.IsValid)
            {
                await db.CatalogItems.AddAsync(item);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToRoute("ListProducts");
            }
            else
            {
                return View(item);
            }
        }


        [HttpGet, Route("details/{id}", Name ="ProductDetail" )]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            var item = await db.CatalogItems.FindAsync(id);
            return View(item);
        }

        //POST /search/b/samsung/c/mobiles
        //POST /search/b/LG/c/refregerator
        [HttpPost("search/b/{brand}/c/{category}", Name ="SearchProduct")]
        public IActionResult Search(string brand, string category)
        {
            return View();
        }
    }
}