using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Models;

namespace AdminPanel.Controllers
{


    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users=await userManager.Users.Select(u=>new UserViewModel
            {
                Id = u.Id,
                FName= u.FName,
                LName= u.LName,
                UserName= u.UserName,
                Email=u.Email,
                Roles= userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(users);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {
            var user =await userManager.FindByIdAsync(id);
            if(user==null)
                return NotFound();
            var allRoles = await roleManager.Roles.ToListAsync();

              var viewModel = new UserRolesViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                   Roles= allRoles.Select(role=> new RoleViewModel
                   {
                       RoleId= role.Id,
                       RoleName= role.Name,
                       IsSelected= userManager.IsInRoleAsync(user,role.Name).Result,
                   }).ToList(),
                };
                return View(viewModel);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user=await userManager.FindByIdAsync(model.UserId);
            var userRole = await userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if(userRole.Any(r=>r == role.RoleName) && !role.IsSelected)
                {
                    await userManager.RemoveFromRoleAsync(user,role.RoleName);
                }
                if (!userRole.Any(r => r == role.RoleName) && role.IsSelected)
                {
                    await userManager.AddToRoleAsync(user, role.RoleName);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
