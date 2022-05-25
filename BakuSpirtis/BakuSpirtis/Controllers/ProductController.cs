using BakuSpirtis.Data;
using BakuSpirtis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = _context.Products.Where(p => p.IsDeleted == false).Count();
            List<Product> products = await _context.Products.Where(p => p.IsDeleted == false)
               .Include(m => m.Category)
               .OrderByDescending(m => m.Id)
               .ToListAsync();

            return View(products);
        }
    }
}
