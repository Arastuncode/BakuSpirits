using System.Linq;
using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.Services;
using BakuSpirtis.ViewModels; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

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
        public async Task<IActionResult> Search(string search, string searchBy)
        {
            List<Product> products = await _context.Products.Include(m=>m.ProductImages).Where(m=>m.IsDeleted==false).ToListAsync();
            List<Product> wantedProducts = new List<Product> { };
            foreach (var item in products)
            {
                if (item.Name.ToLower().Trim().Contains(search.ToLower().Trim()))
                {
                    wantedProducts.Add(item);
                }
            }
            return View(wantedProducts);
        }

    }
}
