using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;
using VseTShirts.Areas.Admin.Models;
using VseTShirts;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using Microsoft.AspNetCore.Authorization;


namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles=Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        public OrderController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            List<Order> orders = ordersStorage.GetAll();
            return View(Helper.ToViewModel(orders));
        }
        public IActionResult UpdateStatus(Guid id, OrderStatus status)
        {
            ordersStorage.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DelOrder(Guid id)
        {
            ordersStorage.RemoveById(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
