using BakuSpirtis.Models;
using BakuSpirtis.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BakuSpirtis.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser newUser = new AppUser()
            {
                Name = registerVM.Name,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = newUser.Id, token = token }, Request.Scheme, Request.Host.ToString());

            await SendEmail(newUser.Email,link);

            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("EmialVerification");
        }
        //

        public async Task<IActionResult> VerifyEmail(string userId,string token)
        {
            return Ok();

        }
        public IActionResult EmialVerification()
        {

            return View();
        }

        public  async Task SendEmail(string email, string url)
        {
           
        }





        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
          
            if (!ModelState.IsValid) return View(loginVM);
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UsernameorEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameorEmail);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Email və ya istifadəçı adı yanlışdır");
                return View();
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email və ya istifadəçı adı yanlışdır");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
       
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
