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
        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var company = await GetCompanyById(id);
            if (company is null) return NotFound();
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Company company = await _context.Companies.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            Company company = await _context.Companies.Where(m => m.Id == id).FirstOrDefaultAsync();
            CompanyVM companyVM = new CompanyVM
            {
                Image = company.Image,
                Decs = company.Desc,
            };
            if (companyVM is null) return View();
            return View(companyVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyVM companyVM)
        {
            var dbCompany = await GetCompanyById(id);
            if (dbCompany is null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (!companyVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbCompany);
            }
            if (!companyVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(companyVM);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbCompany.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + companyVM.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await companyVM.Photo.CopyToAsync(stream);
            }
            dbCompany.Image = fileName;
            dbCompany.Desc = dbCompany.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
