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
    public class CardsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CardsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Card> cards = await _context.Cards.ToListAsync();
            return View(cards);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CardVM cardVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Desc"].ValidationState == ModelValidationState.Invalid) return View();
            if (!cardVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View();
            }
            if (!cardVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + cardVM.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await cardVM.Photo.CopyToAsync(stream);
            }
            Card cards = new Card
            {
                Image = fileName,
                Desc = cardVM.Desc,
                Name=cardVM.Name,

            };
            await _context.Cards.AddAsync(cards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<Card> GetCardById(int id)
        {
            return await _context.Cards.FindAsync(id);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var cards = await GetCardById(id);
            if (cards is null) return NotFound();
            return View(cards);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Card cards = await _context.Cards.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Cards.Remove(cards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            Card cards = await _context.Cards.Where(m => m.Id == id).FirstOrDefaultAsync();
            CardVM cardVM = new CardVM
            {
                Image = cards.Image,
                Name=cards.Name,
                Desc = cards.Desc,
            };
            if (cardVM is null) return View();
            return View(cardVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CardVM cardVM)
        {
            var dbCard = await GetCardById(id);
            if (dbCard is null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (!cardVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbCard);
            }
            if (!cardVM.Photo.CheckFileSize(50000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(cardVM);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbCard.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + cardVM.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await cardVM.Photo.CopyToAsync(stream);
            }
            dbCard.Image = fileName;
            dbCard.Name = cardVM.Name;
            dbCard.Desc = cardVM.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
