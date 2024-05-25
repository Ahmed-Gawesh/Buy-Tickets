using BuyMovies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewModel model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction(nameof(LoginAdmin));
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);


            if (result.Succeeded || !await userManager.IsInRoleAsync(user, "Admin")) 
            {
                return RedirectToAction(nameof(Login));
            }
           
            if (!result.Succeeded || !await userManager.IsInRoleAsync(user,"Admin"))
            {
                ModelState.AddModelError(string.Empty, "You Are UnAuthorize");
                return RedirectToAction(nameof(LoginAdmin));
            }
            
            return RedirectToAction("Index", "Home");
           
          
        }
    }
}
