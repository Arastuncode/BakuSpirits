using AspProject.Utilities.Helper;
using BakuSpirtis.Models;
using BakuSpirtis.ViewModels.Account; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using IEmailService = BakuSpirtis.Services.Interfaces.IEmailService;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BakuSpirtis.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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

            await _userManager.AddToRoleAsync(newUser, UserRoles.Member.ToString());

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = newUser.Id, token = token }, Request.Scheme, Request.Host.ToString());

            string html = $"<a href ={link}> Click here</a>";
            string content = "Email for register confirmation";
            await _emailService.SendEmail(newUser.Email, newUser.UserName, html, content);
            await _signInManager.SignInAsync(newUser, false);
            //await SendEmail(newUser.Email,link);
            //return RedirectToAction("EmialVerification");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> VerifyEmail(string userId,string token)
        {
            if (userId == null || token == null) return BadRequest();
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EmialVerification()
        {

            return View();
        }

        //public  async Task SendEmail(string email, string url)
        //{
            
        //    var apiKey = "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";
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("memisovelman60@gmail.com", "Elman");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress(email, "Example User");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    var htmlContent = $"<a href ={url}> Click here</a>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
      
         
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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

       //[Authorize(Roles = "Admin")]
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (!ModelState.IsValid) return View(forgotPasswordVM);
            var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Bu email qeydiyyat olunmayıb");
                return View(forgotPasswordVM);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = code }, Request.Scheme, Request.Host.ToString());
            string html = $"<a href ={link}> Click here</a>";
            string content = "Email for forgot password";
            await _emailService.SendEmail(user.Email, user.UserName, html, content);

            return RedirectToAction(nameof(ForgotPasswordConfirm));
        }

        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordVM 
            {
                Email=email,
                Token=token
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid) return View(resetPasswordVM);
            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user is null) return NotFound();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(resetPasswordVM);
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgotPasswordConfirm()
        {
            return View();
        }
    }
}
