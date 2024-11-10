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
        public async Task<IActionResult> IndexAsync()
        {
            List<Order> orders = await ordersStorage.GetAllAsync();
            var ordersVM = Helper.ToViewModel(orders);
            return View();
        }
        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatus status)
        {
            await ordersStorage.UpdateStatusAsync(id, status);
            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<IActionResult> DelOrderAsync(Guid id)
        {
            await ordersStorage.RemoveByIdAsync(id);
            return RedirectToAction(nameof(IndexAsync));
        }

    }
}
