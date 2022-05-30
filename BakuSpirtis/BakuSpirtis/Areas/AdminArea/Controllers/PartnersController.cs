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
    public class PartnersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PartnersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Partners> partners = await _context.Partners.ToListAsync();
            return View(partners);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnersVM partnersVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Decs"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid) return View();
            if (!partnersVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View();
            }
            if (!partnersVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + partnersVM.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await partnersVM.Photo.CopyToAsync(stream);
            }
            Partners partners = new Partners
            {
                Image = fileName,
                Name =partnersVM.Name,
                Desc = partnersVM.Decs,

            };
            await _context.Partners.AddAsync(partners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<Partners> GetPartnersById(int id)
        {
            return await _context.Partners.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var partners = await GetPartnersById(id);
            if (partners is null) return NotFound();
            return View(partners);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Partners partners = await _context.Partners.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Partners.Remove(partners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            Partners partners = await _context.Partners.Where(m => m.Id == id).FirstOrDefaultAsync();
            PartnersVM partnersVM = new PartnersVM
            {
                Image = partners.Image,
                Name = partners.Name,
                Decs =partners.Desc
            };
            if (partnersVM is null) return View();
            return View(partnersVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartnersVM partnersVM)
        {
            var dbPartners = await GetPartnersById(id);
            if (dbPartners is null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (!partnersVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbPartners);
            }
            if (!partnersVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(partnersVM);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbPartners.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + partnersVM.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await partnersVM.Photo.CopyToAsync(stream);
            }
            dbPartners.Image = fileName;
            dbPartners.Name = partnersVM.Name;
            dbPartners.Desc = partnersVM.Decs;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
