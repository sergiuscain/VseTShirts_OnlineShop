using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var model =  new UserIndexViewModel { Users = users.Select(u => u.ToViewModel()).ToList(), Roles = roles.Select(r => new RoleViewModel { Name = r.Name }).ToList() };
            return View(model);
        }
        public async Task<IActionResult> ChangePasswordAsync(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            var changeData = new ChangePasswordViewModel { Email = Email,  OldPassword = "@Admin2024!!"};
            return View(changeData);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            var user = await userManager.FindByEmailAsync(data.Email);
            var newHashPassword = userManager.PasswordHasher.HashPassword(user, data.NewPassword);
            user.PasswordHash = newHashPassword;
            await userManager.UpdateAsync(user);
            return View("passwordSuccessfullyChanged");
        }
            public IActionResult Block(uint id)
        {
            return View();
        }
        public async Task<IActionResult> DelAsync(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditRoleAsync(string RoleName ,string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            var userRoles = await userManager.GetRolesAsync(user);
            user.Role = RoleName;
            var result = await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRoleAsync(user, RoleName);
            return RedirectToAction("Index");
        }
    }
}
