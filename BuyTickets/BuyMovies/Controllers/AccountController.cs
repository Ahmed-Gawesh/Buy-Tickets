using BuyMovies.Helpers;
using BuyMovies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;
using System.Diagnostics;

namespace BuyMovies.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IEmailSettings emailSettings;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IEmailSettings emailSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
			this.emailSettings = emailSettings;
		}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree= model.IsAgree,
                };
                var result=await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
               
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                   
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               var user=await userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var flag = await userManager.CheckPasswordAsync(user,model.Password);
                    if(flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);  
                        if (result.Succeeded)
                            return  RedirectToAction("Index","Movies");
                    }
                    ModelState.AddModelError(string.Empty, "Password is Invalid");
                }
                ModelState.AddModelError(string.Empty, "Email is Invalid");
            }
            return View(model);
        }

        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                var token=await userManager.GeneratePasswordResetTokenAsync(user);
                var passwrodLink = Url.Action("ResetPassword", "Account", new { email = user.Id,token }, Request.Scheme);
                if (user is not null)
                {
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To=user.Email,
                        Body=passwrodLink
                    };
                    emailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                   
                }
                ModelState.AddModelError(string.Empty, "Email Is Invalid");
            }
            return View(model);

        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"]= token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user=await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result=await userManager.ResetPasswordAsync(user, token,model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

    }
}
