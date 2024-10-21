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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl == null) returnUrl = "/Home";
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



        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterModel() {ReturnUrl = returnUrl});
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
               User user = new User {Email = register.Email, UserName = register.UserName, PhoneNumber = register.PhoneNumber};
               var result = _usersManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager .SignInAsync(user, false).Wait();
                    _usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    return Redirect(register.ReturnUrl?? "/Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(register);
        }
        public  IActionResult Logout()
        {
             _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
