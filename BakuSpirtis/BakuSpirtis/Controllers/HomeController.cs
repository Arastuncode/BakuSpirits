using BakuSpirtis.Data;
using BakuSpirtis.Models;
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
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
              
            };

            return View(homeVM);
        }

        
    }
}
