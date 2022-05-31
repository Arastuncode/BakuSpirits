using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.Services;
using BakuSpirtis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class ProductionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;

        public ProductionController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {

            List<Process> processes = await _context.Processes.ToListAsync();
            List<Card> cards = await _context.Cards.ToListAsync();
            ProductionVM productionVM = new ProductionVM
            {
               Processes = processes,
               Cards=cards,
            };

            return View(productionVM);
        }
    }
}
