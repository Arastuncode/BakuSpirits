using BakuSpirtis.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class PartnersController : Controller
    {
        private readonly AppDbContext _context;
        public PartnersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var partners = await _context.Partners.ToListAsync();
            return View(partners);
        }
    }
}
