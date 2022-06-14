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
    public class AdvertisementController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdvertisementController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            return View(advertisments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertisementVM advertisementVM)
        {
            if (ModelState["Video"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var video in advertisementVM.Video)
            {
                if (!video.CheckFileType("video/"))
                {
                    ModelState.AddModelError("Video", "Video type is wrong");
                    return View();
                }

                if (!video.CheckFileSize(500000))
                {
                    ModelState.AddModelError("Video", "Video size is wrong");
                    return View();
                }

            }
            foreach (var video in advertisementVM.Video)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + video.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/video", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }


                Advertisment advertisment = new Advertisment
                {
                    Video = fileName
                };

                await _context.Advertisments.AddAsync(advertisment);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<Advertisment> GetAdvertismentsById(int id)
        {
            return await _context.Advertisments.FindAsync(id);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Advertisment advertisment = await _context.Advertisments.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (advertisment is null) return NotFound();
            _context.Advertisments.Remove(advertisment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
