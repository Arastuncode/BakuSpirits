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
    public class CompanyVideoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CompanyVideoController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<CompanyVideo> videos = await _context.CompanyVideos.ToListAsync();
            return View(videos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyVideoVM companyVideoVM)
        {
            if (ModelState["Video"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var item in companyVideoVM.Video)
            {
                if (!item.CheckFileType("video/"))
                {
                    ModelState.AddModelError("Video", "Video type is wrong");
                    return View();
                }

                if (!item.CheckFileSize(500000))
                {
                    ModelState.AddModelError("Video", "Video size is wrong");
                    return View();
                }

            }
            foreach (var item in companyVideoVM.Video)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/video", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }


                CompanyVideo companyVideo = new CompanyVideo
                {
                    Video = fileName
                };

                await _context.CompanyVideos.AddAsync(companyVideo);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<CompanyVideo> GetCompanyVideoById(int id)
        {
            return await _context.CompanyVideos.FindAsync(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CompanyVideo video = await _context.CompanyVideos.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (video is null) return NotFound();
            _context.CompanyVideos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
