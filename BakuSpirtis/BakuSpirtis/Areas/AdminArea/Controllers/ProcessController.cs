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
    public class ProcessController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProcessController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Process> processes = await _context.Processes.ToListAsync();
            return View(processes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcessVM processVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Text"].ValidationState == ModelValidationState.Invalid) return View();
            if (!processVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View();
            }
            if (!processVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + processVM.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await processVM.Photo.CopyToAsync(stream);
            }
            Process process = new Process
            {
                Image = fileName,
                Text = processVM.Text,

            };
            await _context.Processes.AddAsync(process);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<Process> GetProcessById(int id)
        {
            return await _context.Processes.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var process = await GetProcessById(id);
            if (process is null) return NotFound();
            return View(process);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Process process = await _context.Processes.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            Process process = await _context.Processes.Where(m => m.Id == id).FirstOrDefaultAsync();
            ProcessVM processVM = new ProcessVM
            {
                Image = process.Image,
                Text = process.Text,
            };
            if (processVM is null) return View();
            return View(processVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcessVM processVM)
        {
            var dbProcess = await GetProcessById(id);
            if (dbProcess is null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (!processVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbProcess);
            }
            if (!processVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(processVM);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbProcess.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + processVM.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await processVM.Photo.CopyToAsync(stream);
            }
            dbProcess.Image = fileName;
            dbProcess.Text = processVM.Text;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
