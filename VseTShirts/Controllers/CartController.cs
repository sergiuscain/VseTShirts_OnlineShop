using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IProductsStorage productStorage;
        private readonly UserManager<User> userManager;

        public CartController(ICartsStorage cartsStorage, IProductsStorage productStorage, UserManager<User> userManager)
        {
            this.productStorage = productStorage;
            this.cartsStorage = cartsStorage;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            var cart = cartsStorage.GetCartByUserId(user.Id);
            return View(cart.ToViewModel());
        }
        public IActionResult Add(Guid Id)
        {
            Product product = productStorage.GetById(Id);
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            cartsStorage.Add(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid Id)
        {
            var product = productStorage.GetById(Id);
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            cartsStorage.Remove(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public IActionResult RemovePosition(Guid Id)
        {
            var product = productStorage.GetById(Id);
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            cartsStorage.RemovePosition(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveAll()
        {
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            cartsStorage.RemoveAll(user.Id);
            return RedirectToAction("Index");
        }
    }
}
