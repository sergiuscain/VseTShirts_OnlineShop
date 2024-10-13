using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;

namespace VseTShirts.Views.Shared.ViewComponents.CartViewComponents
{
    public class CalcFavoriteProductsCountViewComponent : ViewComponent
    {
        private readonly IFavoriteProductsStorage _favoriteProductsStorage;
        public CalcFavoriteProductsCountViewComponent(IFavoriteProductsStorage favoriteProductsStorage)
        {
            _favoriteProductsStorage = favoriteProductsStorage;
        }
        public IViewComponentResult Invoke()
        {
            int productsCount = _favoriteProductsStorage.GetByUserId(Constants.UserId).Count;
            return View("FavoriteProductsCountView",productsCount);
        }
    }
}
