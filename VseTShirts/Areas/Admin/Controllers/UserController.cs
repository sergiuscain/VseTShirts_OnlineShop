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
        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users.Select(u => u.ToViewModel()).ToList());
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
        public IActionResult Del(uint id)
        {
            return View();
        }
        public IActionResult EditRole(uint id)
        {
            return View();
        }
    }
}
