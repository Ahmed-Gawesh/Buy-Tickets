using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BuyMovies.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles=await roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                var roleExists = await roleManager.RoleExistsAsync(model.Name);
                if(!roleExists)
                {
                    var role = await roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
                    return RedirectToAction(nameof(Index),await roleManager.Roles.ToListAsync());
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exist");
                    return RedirectToAction(nameof(Index), await roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role =await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role=await roleManager.FindByIdAsync(id);
            var mappedRole = new RoleViewModel()
            {
                RoleName = role.Name,
            };
            return View(mappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var roleExists=await roleManager.RoleExistsAsync(id);
                if(!roleExists)
                {
                    var role = await roleManager.FindByIdAsync(id);
                    role.Name=model.RoleName;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index),await roleManager.Roles.ToListAsync());
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exist");
                    return RedirectToAction(nameof(Index), await roleManager.Roles.ToListAsync());
                }

            }
            return RedirectToAction(nameof(Index));
            
        }


    }
}
