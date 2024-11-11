using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VseTShirts.Areas.Admin.Models;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles.Select(r => new RoleViewModel { Name = r.Name }).ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleViewModel role)
        {
            if ((await roleManager.FindByNameAsync(role.Name)) != null)
            {
                ModelState.AddModelError("Name", "Роль уже существует!");
            }
            else if (role.Name.Length > 30)
            {
                ModelState.AddModelError("Name", "Слишком длинное название роли");
            }
            else if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(role);
        }

        public async Task<IActionResult> RemoveAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}
