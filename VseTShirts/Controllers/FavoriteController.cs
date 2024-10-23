using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;

namespace VseTShirts.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteProductsStorage _favoriteProductsStorage;
        private readonly IProductsStorage _productsStorage;
        private readonly UserManager<User> _userManager;
        public FavoriteController(IFavoriteProductsStorage favoriteProductsStorage, IProductsStorage productsStorage, UserManager<User> userManager)
        {
            _favoriteProductsStorage = favoriteProductsStorage;
            _productsStorage = productsStorage;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var favoriteProducts = _favoriteProductsStorage.GetByUserId(user.Id);
            var favoriteProductsViewModel = Helper.ToViewModel(favoriteProducts);
            return View(favoriteProductsViewModel);
        }
        public IActionResult Add(Guid Id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var product = _productsStorage.GetById(Id);
            _favoriteProductsStorage.Add(user.Id, product);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid Id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var product = _productsStorage.GetById(Id);
            _favoriteProductsStorage.Remove(user.Id, product);
            return RedirectToAction("Index");
        }
    }
}
