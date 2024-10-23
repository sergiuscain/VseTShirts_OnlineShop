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
        public IActionResult Buy(DeliveryInfo deliveryInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(deliveryInfo);
            }
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            var order = new Order
            {
                Address = deliveryInfo.Address,
                Positions = cartsStorage.GetCartByUserId(user.Id).Positions,
                Name = deliveryInfo.Name,
                Phone = deliveryInfo.Phone,
                UserId = user.Id
            };
            ordersStorage.Add(order);
            cartsStorage.RemoveAll(user.Id);
            return View(deliveryInfo);
        }

        public IActionResult Order(Guid id)
        {
            var order = ordersStorage.GetById(id);
            return View(order.ToViewModel());
        }
    }
}
