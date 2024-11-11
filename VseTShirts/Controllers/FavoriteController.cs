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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favoriteProducts = await _favoriteProductsStorage.GetByUserIdAsync(user.Id);
            var favoriteProductsViewModel = Helper.ToViewModel(favoriteProducts);
            return View(favoriteProductsViewModel);
        }
        public async Task<IActionResult> AddAsync(Guid Id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var product = await _productsStorage.GetByIdAsync(Id);
            await _favoriteProductsStorage.AddAsync(user.Id, product);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAsync(Guid Id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var product = await _productsStorage.GetByIdAsync(Id);
            await _favoriteProductsStorage.RemoveAsync(user.Id, product);
            return RedirectToAction("Index");
        }
    }
}
