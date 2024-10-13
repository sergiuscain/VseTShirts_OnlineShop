using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;

namespace VseTShirts.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAccountManager _accountManager;
        public UserController(IAccountManager _accountManager)
        {
            this._accountManager = _accountManager;
        }

        public IActionResult Index()
        {
            var users = _accountManager.GetAll();
            return View(users);
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
