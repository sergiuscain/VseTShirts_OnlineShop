using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;

namespace VseTShirts.Views.Shared.Components.CalcCartCount
{
    public class CalcCartCountViewComponent : ViewComponent
    {
        private readonly ICartsStorage _cartStorage;
        public CalcCartCountViewComponent(ICartsStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }
        public IViewComponentResult Invoke()
        {
            var cart = _cartStorage.GetCartByUserId(Constants.UserId);//.Positions.Count();
            var positions = cart == null ? null : cart.Positions;
            int count = positions == null ? 0 : Mapping.ToViewModel(cart).productsCountInCart;
            return View("CalcCartCountViewComponent", count);
        }
    }
}
