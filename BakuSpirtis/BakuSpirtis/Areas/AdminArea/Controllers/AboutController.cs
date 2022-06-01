using AspProject.Utilities.File;
using AspProject.Utilities.Helper;
using BakuSpirtis.Data;
using BakuSpirtis.Models;
using BakuSpirtis.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles ="Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _context.Abouts.ToListAsync();
            return View(abouts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutVM aboutVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Text"].ValidationState == ModelValidationState.Invalid) return View();
            if (!aboutVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View();
            }
            if (!aboutVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + aboutVM.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await aboutVM.Photo.CopyToAsync(stream);
            }
            About about = new About
            {
                Image = fileName,
                Text = aboutVM.Text,
              
            };
            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<About> GetAboutById(int id)
        {
            return await _context.Abouts.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var about = await GetAboutById(id);
            if (about is null) return NotFound();
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            About about = await _context.Abouts.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            About about = await _context.Abouts.Where(m => m.Id == id).FirstOrDefaultAsync();
            AboutVM aboutVM = new AboutVM
            {
                Image = about.Image,
                Text=about.Text,
            };
            if (aboutVM is null) return View();
            return View(aboutVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutVM aboutVM)
        {
            var dbAbout = await GetAboutById(id);
            if (dbAbout is null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (!aboutVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbAbout);
            }
            if (!aboutVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(aboutVM);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbAbout.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + aboutVM.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await aboutVM.Photo.CopyToAsync(stream);
            }
            dbAbout.Image = fileName;
            dbAbout.Text = aboutVM.Text;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
