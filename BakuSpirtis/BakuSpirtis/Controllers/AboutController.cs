using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.Services;
using BakuSpirtis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
       
        public AboutController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;          
        }
        public async Task<IActionResult> Index()
        {

            List<Company> companies = await _context.Companies.ToListAsync();
            List<Corusel> corusels = await _context.Corusels.ToListAsync();
            List<CompanyVideo> companyVideos = await _context.CompanyVideos.ToListAsync();
            List<Sertification> sertifications = await _context.Sertifications.ToListAsync();
            List<About> abouts = await _context.Abouts.ToListAsync();

            CompaniesVM companyVM = new CompaniesVM
            {
               Companies=companies,
               Corusels =corusels,
               CompanyVideos = companyVideos, 
               Sertifications= sertifications,
               Abouts = abouts,
            };

            return View(companyVM);
        }

    }
}
