using Microsoft.AspNetCore.Mvc;
using VseTShirts.Areas.Admin.Models;
using VseTShirts.Models;

namespace VseTShirts.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesStorage rolesStorage;
        public RoleController(IRolesStorage rolesStorage)
        {
            this.rolesStorage = rolesStorage;
        }

        public IActionResult Index()
        {
            var roles = rolesStorage.GetAll();
            return View(roles);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            if (rolesStorage.GetByName(role.Name) != null)
            {
                ModelState.AddModelError("Name", "Роль уже существует!");
            }
            else if (ModelState.IsValid)
            {
                rolesStorage.Add(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public ActionResult Remove(string name)
        {
            rolesStorage.Remove(rolesStorage.GetByName(name));
            return RedirectToAction(nameof(Index));
        }
    }
}
