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
    public class CertificationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CertificationController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Sertification> sertifications = await _context.Sertifications.ToListAsync();
            return View(sertifications);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CertificationVM sertificationVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in sertificationVM.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Image type is wrong");
                    return View();
                }

                if (!photo.CheckFileSize(50000))
                {
                    ModelState.AddModelError("Photo", "Image size is wrong");
                    return View();
                }

            }
            foreach (var photo in sertificationVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }


                Sertification sertification = new Sertification
                {
                    Image = fileName
                };

                await _context.Sertifications.AddAsync(sertification);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sertification = await GetSertificationById(id);
            if (sertification is null) return NotFound();
            CertificationVM sertificationVM = new CertificationVM
            {
                Image = sertification.Image,

            };

            return View(sertificationVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Sertification sertification)
        {
            var dbSertification = await GetSertificationById(Id);
            if (dbSertification == null) return NotFound();

            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            if (!sertification.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbSertification);
            }

            if (!sertification.Photos.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbSertification);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbSertification.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + sertification.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await sertification.Photos.CopyToAsync(stream);
            }

            dbSertification.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private async Task<Sertification> GetSertificationById(int id)
        {
            return await _context.Sertifications.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var sertification = await GetSertificationById(id);
            if (sertification is null) return NotFound();
            return View(sertification);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Sertification sertification = await _context.Sertifications.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (sertification is null) return NotFound();
            _context.Sertifications.Remove(sertification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
