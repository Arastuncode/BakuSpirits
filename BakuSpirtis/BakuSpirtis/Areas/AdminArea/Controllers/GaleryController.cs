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
    [Authorize(Roles = "Admin")]
    public class GaleryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public GaleryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Galery> galeries = await _context.Galeries.ToListAsync();
            return View(galeries);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GaleryVM galeryVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in galeryVM.Photos)
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
            foreach (var photo in galeryVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }


                Galery galery = new Galery
                {
                    Image = fileName
                };

                await _context.Galeries.AddAsync(galery);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var galery = await GetGaleryById(id);
            if (galery is null) return NotFound();
            GaleryVM galeryVM = new GaleryVM
            {
                Image = galery.Image,

            };

            return View(galeryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Galery galery)
        {
            var dbGalery = await GetGaleryById(Id);
            if (dbGalery == null) return NotFound();

            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            if (!galery.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbGalery);
            }

            if (!galery.Photos.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbGalery);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbGalery.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + galery.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await galery.Photos.CopyToAsync(stream);
            }

            dbGalery.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private async Task<Galery> GetGaleryById(int id)
        {
            return await _context.Galeries.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await GetGaleryById(id);
            if (slider is null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Galery galery = await _context.Galeries.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (galery is null) return NotFound();
            _context.Galeries.Remove(galery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
