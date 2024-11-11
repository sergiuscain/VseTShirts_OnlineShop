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
        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var cart = await cartsStorage.GetCartByUserIdAsync(user.Id);
            return View(cart.ToViewModel());
        }
        public async Task<IActionResult> AddAsync(Guid Id)
        {
            Product product = await productStorage.GetByIdAsync(Id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await cartsStorage.AddAsync(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAsync(Guid Id)
        {
            var product = await productStorage.GetByIdAsync(Id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await cartsStorage.RemoveAsync(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemovePositionAsync(Guid Id)
        {
            var product = await productStorage.GetByIdAsync(Id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await cartsStorage.RemovePositionAsync(product.Id, user.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAllAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await cartsStorage.RemoveAllAsync(user.Id);
            return RedirectToAction("Index");
        }
    }
}
