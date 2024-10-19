using Microsoft.AspNetCore.Authorization;
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
        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
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
            var order = new Order
            {
                Address = deliveryInfo.Address,
                Positions = cartsStorage.GetCartByUserId(Constants.UserId).Positions,
                Name = deliveryInfo.Name,
                Phone = deliveryInfo.Phone,
                UserId = Constants.UserId
            };
            ordersStorage.Add(order);
            cartsStorage.RemoveAll(Constants.UserId);
            return View(deliveryInfo);
        }

        public IActionResult Order(Guid id)
        {
            var order = ordersStorage.GetById(id);
            return View(order.ToViewModel());
        }
    }
}
