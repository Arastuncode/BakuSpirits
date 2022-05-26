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
            
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<About> abouts = await _context.Abouts.ToListAsync();
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            List<Product> products = await _context.Products.Where(p => p.IsDeleted == false)
                .Include(m => m.ProductImages)
                .OrderByDescending(m => m.Id)
                .Take(4)
                .ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Abouts=abouts,
                Products = products,
                Advertisments=advertisments,
            };

            return View(homeVM);
        }

        
    }
}
