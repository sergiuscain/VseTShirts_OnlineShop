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
        public IActionResult ChangePassword(string Email)
        {
            var user = userManager.FindByEmailAsync(Email).Result;
            var changeData = new ChangePasswordViewModel { Email = Email,  OldPassword = "@Admin2024!!"};
            return View(changeData);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            var user = await userManager.FindByEmailAsync(data.Email);
            var newHashPassword = userManager.PasswordHasher.HashPassword(user, data.NewPassword);
            user.PasswordHash = newHashPassword;
            userManager.UpdateAsync(user).Wait();
            return View("passwordSuccessfullyChanged");
        }
            public IActionResult Block(uint id)
        {
            return View();
        }
        public IActionResult Del(string Email)
        {
            var user = userManager.FindByEmailAsync(Email);
            userManager.DeleteAsync(user.Result).Wait();
            return RedirectToAction("Index");
        }
        public IActionResult EditRole(string RoleName ,string Email)
        {
            var user = userManager.FindByEmailAsync(Email).Result;
            var lastRole = userManager.GetRolesAsync(user).Result.FirstOrDefault();//
            user.Role = RoleName;
            userManager.RemoveFromRoleAsync(user, lastRole).Wait();
            var s = userManager.AddToRoleAsync(user, RoleName).Result;
            return RedirectToAction("Index");
        }
    }
}
