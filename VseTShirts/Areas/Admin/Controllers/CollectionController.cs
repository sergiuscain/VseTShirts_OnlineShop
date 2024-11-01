using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;

namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class CollectionController : Controller
    {
        private readonly ICollectionsStorage _collections;
        public CollectionController(ICollectionsStorage collections)
        {
            _collections = collections;
        }
        public IActionResult Index()
        {
            var collections = _collections.GetAll();
            var collectionsVM = collections.Select(c => c.ToViewModel()).ToList();
            return View(collectionsVM);
        }
        [HttpPost]
        public IActionResult Add(int id, string description, string name)
        {
            _collections.Add(new DB.Models.Collection { Count = 0, Name = name, Description = description });
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _collections.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
