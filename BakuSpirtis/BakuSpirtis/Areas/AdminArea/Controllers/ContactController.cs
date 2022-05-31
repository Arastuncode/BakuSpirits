using BakuSpirtis.Data;
using BakuSpirtis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.AsNoTracking().Where(m => !m.IsDeleted).ToListAsync();
            return View(contacts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isAddress = _context.Contacts.Where(m => !m.IsDeleted).Any(m => m.Address.ToLower().Trim() == contact.Address.ToLower().Trim());
            if (isAddress)
            {
                ModelState.AddModelError("Address", "Bu address artiq movcuddur");
                return View();
            }
            bool isEmail = _context.Contacts.Where(m => !m.IsDeleted).Any(m => m.Email.ToLower().Trim() == contact.Email.ToLower().Trim());
            if (isEmail)
            {
                ModelState.AddModelError("Email", "Bu email artiq movcuddur");
                return View();
            }
            bool isPhone = _context.Contacts.Where(m => !m.IsDeleted).Any(m => m.Phone.ToLower().Trim() == contact.Phone.ToLower().Trim());
            if (isPhone)
            {
                ModelState.AddModelError("Phone", "Bu phone artiq movcuddur");
                return View();
            }

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Contact contact = await _context.Contacts.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
