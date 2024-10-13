using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;

namespace VseTShirts.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteProductsStorage _favoriteProductsStorage;
        private readonly IProductsStorage _productsStorage;
        public FavoriteController(IFavoriteProductsStorage favoriteProductsStorage, IProductsStorage productsStorage)
        {
            _favoriteProductsStorage = favoriteProductsStorage;
            _productsStorage = productsStorage;
        }
        public IActionResult Index()
        {
            var favoriteProducts = _favoriteProductsStorage.GetByUserId(Constants.UserId);
            var favoriteProductsViewModel = Helper.ToViewModel(favoriteProducts);
            return View(favoriteProductsViewModel);
        }
        public IActionResult Add(Guid Id)
        {
            var product = _productsStorage.GetById(Id);
            _favoriteProductsStorage.Add(Constants.UserId, product);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid Id)
        {
            var product = _productsStorage.GetById(Id);
            _favoriteProductsStorage.Remove(Constants.UserId, product);
            return RedirectToAction("Index");
        }
    }
}
