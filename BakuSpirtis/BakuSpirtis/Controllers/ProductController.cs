using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.Services;
using BakuSpirtis.ViewModels;
using BakuSpirtis.ViewModels.Admin;
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
        private readonly LayoutService _layoutService;
        private readonly ProductService _productService;
        public ProductController(AppDbContext context, LayoutService layoutService, ProductService productService)
        {
            _context = context;
            _layoutService = layoutService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {

            List<Category> categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            List<Product> products = await _context.Products.Where(p => p.IsDeleted == false)
                 .Include(m => m.Category)
                 .Include(m => m.ProductImages)
                 .OrderByDescending(m => m.Id)
                 .Take(8)
                 .ToListAsync();
            ProductVM productVM = new ProductVM
            {
                Products=products,
                Categories=categories,
            };

            return View(productVM);
        }
        
         

    }
}
