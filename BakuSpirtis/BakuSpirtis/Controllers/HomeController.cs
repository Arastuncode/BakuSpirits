using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.Services;
using BakuSpirtis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly ProductService _productService;
        public HomeController(AppDbContext context, LayoutService layoutService, ProductService productService)
        {
            _context = context;
            _layoutService = layoutService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
           // Dictionary<string, string> settings = _layoutService.GetSettings();
            //int take = int.Parse(settings["HomeTake"]);
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<About> abouts = await _context.Abouts.ToListAsync();
            //List<Category> categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            //IEnumerable<Product> products = await _productService.GetProducts(take);
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Abouts=abouts,
                //Categories = categories,
                //Products = products,

            };

            return View(homeVM);
        }

        
    }
}
