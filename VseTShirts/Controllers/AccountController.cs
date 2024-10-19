using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;
using VseTShirts.DB.Models;
using VseTShirts.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace VseTShirts.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usersManager;
        //private readonly IUsersManager  usersManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> _usersManager, SignInManager<User> signInManager)
        {
           // this.usersManager = usersManager;
            this._signInManager = signInManager;
            this._usersManager = _usersManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult Login( LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.isRemembMe, false).Result;
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }
                return View(login);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не могут совпадать");
            }
            if (ModelState.IsValid)
            {
               // _accountManager.Register(register);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(Register));
        }
        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
