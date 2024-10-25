﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles.Select(r => new RoleViewModel { Name = r.Name }).ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RoleViewModel role)
        {
            if (roleManager.FindByNameAsync(role.Name).Result != null)
            {
                ModelState.AddModelError("Name", "Роль уже существует!");
            }
            else if (role.Name.Length > 30)
            {
                ModelState.AddModelError("Name", "Слишком длинное название роли");
            }
            else if (ModelState.IsValid)
            {
                var result = roleManager.CreateAsync(new IdentityRole(role.Name)).Result;
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

        public ActionResult Remove(string name)
        {
            var role = roleManager.FindByNameAsync(name).Result;
            roleManager.DeleteAsync(role).Wait();
            return RedirectToAction(nameof(Index));
        }
    }
}
