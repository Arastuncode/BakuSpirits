using BakuSpirtis.Data;
using BakuSpirtis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        public ContactController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }
        public  IActionResult Index()
        {
            Dictionary<string, string> settings = _layoutService.GetSettings();
            return View(settings);
        }

    }
}
