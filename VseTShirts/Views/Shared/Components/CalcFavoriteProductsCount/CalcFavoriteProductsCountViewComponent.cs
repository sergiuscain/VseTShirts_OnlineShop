using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;

namespace VseTShirts.Views.Shared.ViewComponents.CartViewComponents
{
    public class CalcFavoriteProductsCountViewComponent : ViewComponent
    {
        private readonly IFavoriteProductsStorage favoriteProductsStorage;
        private readonly UserManager<User> userManager;

        public CalcFavoriteProductsCountViewComponent(IFavoriteProductsStorage _favoriteProductsStorage, UserManager<User> _userManager)
        {
            favoriteProductsStorage = _favoriteProductsStorage;
            userManager = _userManager;
        }
        public IViewComponentResult Invoke()
        {
            try 
            {
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                int productsCount = favoriteProductsStorage.GetByUserId(user.Id).Count;
                return View("FavoriteProductsCountView",productsCount);
            }
            catch
            {
                return View("FavoriteProductsCountView",0);
            }
        }
    }
}
