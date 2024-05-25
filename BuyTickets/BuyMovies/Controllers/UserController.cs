using AutoMapper;
using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Models;

namespace BuyMovies.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var Users = await userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    UserName=U.UserName,
                    Email = U.Email,
                    Roles = userManager.GetRolesAsync(U).Result
                }).ToListAsync();
                return View(Users);
            }
            else
            {
                var User = await userManager.FindByEmailAsync(email);
                var MappedUser = new UserViewModel()
                {
                    Id = User.Id,
                    FName = User.FName,
                    LName = User.LName,
                    UserName=User.UserName,
                    Email = User.Email,
                    Roles = userManager.GetRolesAsync(User).Result
                };
                return View(new List<UserViewModel>() { MappedUser });
            }
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            var mappedUser = mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(viewName, mappedUser);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {
           
            return await Details(id,"Edit");
  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]string id,UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    user.UserName = userVM.UserName;
                    user.Email = userVM.Email;

                    
                    await userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(userVM);
        }
        public async Task<IActionResult> Delete(string id)  //For Form(View)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel userVM, [FromRoute] string id)  //for Submit
        {
            if (id != userVM.Id)
                return BadRequest();
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userVM);
            }

        }

    }
}
