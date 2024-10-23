using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;

namespace VseTShirts.Views.Shared.Components.CalcCartCount
{
    public class CalcCartCountViewComponent : ViewComponent
    {
        private readonly ICartsStorage _cartStorage;
        private readonly UserManager<User> userManager;
        public CalcCartCountViewComponent(ICartsStorage cartStorage, UserManager<User> userManager)
        {
            _cartStorage = cartStorage;
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                var cart = _cartStorage.GetCartByUserId(user.Id);//.Positions.Count();
                var positions = cart == null ? null : cart.Positions;
                int count = positions == null ? 0 : Helper.ToViewModel(cart).productsCountInCart;
                return View("CalcCartCountViewComponent", count);
            }
            catch
            {
                return View("CalcCartCountViewComponent", 0);
            }
        }
    }
}
