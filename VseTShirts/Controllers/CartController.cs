using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IProductsStorage productStorage;

        public CartController(ICartsStorage cartsStorage, IProductsStorage productStorage)
        {
            this.productStorage = productStorage;
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index()
        {
            var cart = cartsStorage.GetCartByUserId(Constants.UserId);
            return View(cart.ToViewModel());
        }
        public IActionResult Add(Guid Id)
        {
            Product product = productStorage.GetById(Id);
            
            cartsStorage.Add(product.Id, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid Id)
        {
            var product = productStorage.GetById(Id);
            
            cartsStorage.Remove(product.Id, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult RemovePosition(Guid Id)
        {
            Product product = productStorage.GetById(Id);
            cartsStorage.RemovePosition(product.Id, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveAll()
        {
            cartsStorage.RemoveAll(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
