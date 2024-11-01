using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsStorage _productStorage;
        public readonly IComparedProductsStorage _comparedStorage;
        private readonly UserManager<User> _userManager;
        private readonly ICollectionsStorage _collectionsStorage;

        public HomeController(IProductsStorage productStorage, IComparedProductsStorage comparedStorage, UserManager<User> userManager, ICollectionsStorage collectionsStorage)
        {
            _productStorage = productStorage;
            _comparedStorage = comparedStorage;
            _userManager = userManager;
            _collectionsStorage = collectionsStorage;
        }


        public IActionResult Index()
        {
            var filters = new FiltersViewModel
            {
                Category = "All",
                StartPrice = 0,
                EndPrice = 0,
                SortBy = "Price",
                Color = "All",
                Size = "All",
                Sex = "All",
                MinQuantity = 0,
                MaxQuantity = 0,
            };
            var productsViewModel = _productStorage.GetAll().ToViewModel();
            List<CollectionViewModel> collections = _collectionsStorage.GetAll().Select(c => c.ToViewModel()).ToList();
            var homeIndexModel = new HomeIndexViewModel { Products = productsViewModel, Filters = filters, CollectionsList = collections };
            return View(homeIndexModel);
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
        [Authorize]
        public IActionResult Compare(Guid Id)
        {
            var product1 = _productStorage.GetById(Id);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (_comparedStorage.Add(user.Id, product1))
            {
                var products = _comparedStorage.GetByUserId(user.Id);
                if (products.Count < 2)
                    return RedirectToAction("Index");
                else
                    return View(products.ToViewModel());
            }
            return View(_comparedStorage.GetByUserId(user.Id).ToViewModel());
        }

        public IActionResult RemoveCompare()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            _comparedStorage.Delete(user.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string serachTxt)
        {
            var products = _productStorage.GetAll();
            var newProductsList = products.Where(p => p.Name.ToLower().Contains(serachTxt.ToLower())).ToList();
            List<CollectionViewModel> collections = _collectionsStorage.GetAll().Select(c => c.ToViewModel()).ToList();
            var HomeIndexViewModel = new HomeIndexViewModel { Products = newProductsList.ToViewModel(), CollectionsList = collections };
            return View("Index", HomeIndexViewModel);
        }
        public IActionResult Filter(FiltersViewModel filters)
        {
            var products = _productStorage.GetAll();        
            var filtredProducts = _productStorage.Filtr(products, filters.ToDBModel());
            List<CollectionViewModel> collections = _collectionsStorage.GetAll().Select(c => c.ToViewModel()).ToList();
            var homeIndexViewModel = new HomeIndexViewModel { Products = filtredProducts.ToViewModel(), Filters = filters, CollectionsList = collections };
            return View("Index", homeIndexViewModel);
        }
    }
}
