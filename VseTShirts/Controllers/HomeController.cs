using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VseTShirts.DB;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsStorage productStorage;
        public readonly IComparedProductsStorage comparedStorage;

        public HomeController(IProductsStorage productStorage, IComparedProductsStorage comparedStorage)
        {
            this.productStorage = productStorage;
            this.comparedStorage = comparedStorage;
        }


        public IActionResult Index()
        {
                return View(productStorage.GetAll().ToViewModel());
        }

        public IActionResult Privacy(string a)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Compare(Guid Id)
        {
            var product1 = productStorage.GetById(Id);
            if (comparedStorage.Add(Constants.UserId, product1))
            {
                var products = comparedStorage.GetByUserId(Constants.UserId);
                if (products.Count < 2)
                    return RedirectToAction("Index");
                else
                    return View(products.ToViewModel());
            }
            return View(comparedStorage.GetByUserId(Constants.UserId).ToViewModel());
        }

        public IActionResult RemoveCompare()
        {
            comparedStorage.Delete(Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string serachTxt)
        {
            var products = productStorage.GetAll();
            var newProductsList = products.Where(p => p.Name.ToLower().Contains(serachTxt.ToLower())).ToList();
            return View("Index", newProductsList.ToViewModel());
        }
    }
}
