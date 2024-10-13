using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login( LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _accountManager.GetByUserName(login.UserName);
                if (user == null)
                {
                    user = _accountManager.GetByEmail(login.UserName);
                    if (user == null)
                    {
                       ModelState.AddModelError("Error", "Неверный логин или пароль");
                       return View(login);
                    }
                    if (!_accountManager.VerifyPassword(user, login.Password))
                    {
                        ModelState.AddModelError("Error", "Неверный логин или пароль");
                        return View(login);
                    }
                    if (_accountManager.VerifyPassword(user, login.Password))
                    {
                        _accountManager.Login(user);
                    }
                }
                if (!_accountManager.VerifyPassword(user, login.Password))
                {
                    ModelState.AddModelError("Error", "Неверный логин или пароль");
                    return View(login);
                }
                if (_accountManager.VerifyPassword(user, login.Password))
                {
                    _accountManager.Login(user);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }


        public IActionResult Logout()
        {
            return View();
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
                _accountManager.Register(register);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(Register));
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
