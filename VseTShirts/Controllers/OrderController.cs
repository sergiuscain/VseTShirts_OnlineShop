using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IOrdersStorage ordersStorage;
        private readonly UserManager<User> userManager;
        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage, UserManager<User> userManager)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(DeliveryInfo deliveryInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(deliveryInfo);
            }
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var order = new Order
            {
                Address = deliveryInfo.Address,
                Positions = (await cartsStorage.GetCartByUserIdAsync(user.Id)).Positions,
                Name = deliveryInfo.Name,
                Phone = deliveryInfo.Phone,
                UserId = user.Id
            };
            await ordersStorage.AddAsync(order);
            await cartsStorage.RemoveAllAsync(user.Id);
            return View(deliveryInfo);
        }

        public async Task<IActionResult> Order(Guid id)
        {
            var order = await ordersStorage.GetByIdAsync(id);
            return View(order.ToViewModel());
        }
    }
}
