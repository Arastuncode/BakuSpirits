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
    public class CoruselController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CoruselController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Corusel> corusels = await _context.Corusels.ToListAsync();
            return View(corusels);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoruselVM coruselVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in coruselVM.Photos)
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
            foreach (var photo in coruselVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }


                Corusel corusel = new Corusel
                {
                    Image = fileName
                };

                await _context.Corusels.AddAsync(corusel);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var corusel = await GetCoruselById(id);
            if (corusel is null) return NotFound();
            CoruselVM coruselVM = new CoruselVM
            {
                Image = corusel.Image,

            };

            return View(coruselVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Corusel corusel)
        {
            var dbCorusel = await GetCoruselById(Id);
            if (dbCorusel == null) return NotFound();

            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            if (!corusel.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbCorusel);
            }

            if (!corusel.Photos.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbCorusel);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbCorusel.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + corusel.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await corusel.Photos.CopyToAsync(stream);
            }

            dbCorusel.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private async Task<Corusel> GetCoruselById(int id)
        {
            return await _context.Corusels.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var corusel = await GetCoruselById(id);
            if (corusel is null) return NotFound();
            return View(corusel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Corusel corusel = await _context.Corusels.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (corusel is null) return NotFound();
            _context.Corusels.Remove(corusel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
