using AspProject.Utilities.File;
using AspProject.Utilities.Helper;
using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.ViewModels.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CompanyController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Company> companies = await _context.Companies.ToListAsync();
            return View(companies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyVM companyVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Decs"].ValidationState == ModelValidationState.Invalid) return View();
           
            if (!companyVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View();
            }
            if (!companyVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + companyVM.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await companyVM.Photo.CopyToAsync(stream);
            }
            Company company = new Company
            {
                Image = fileName, 
                Desc = companyVM.Decs,

            };
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
