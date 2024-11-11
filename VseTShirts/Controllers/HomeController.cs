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


        public async Task<IActionResult> Index()
        {
            var filters = new FiltersViewModel
            {
                Category = "ALL",
                StartPrice = 0,
                EndPrice = 0,
                SortBy = "Price",
                Color = "ALL",
                Size = "ALL",
                Sex = "ALL",
                MinQuantity = 0,
                MaxQuantity = 0,
            };
            var productsViewModel = (await _productStorage.GetAllAsync()).ToViewModel();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexModel = new HomeIndexViewModel { Products = productsViewModel, Filters = filters, CollectionsList = collections, IsActiveFilters = false};
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
        public async Task<IActionResult> CompareAsync(Guid Id)
        {
            var product1 = await _productStorage.GetByIdAsync(Id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (await _comparedStorage.AddAsync(user.Id, product1))
            {
                var products = await _comparedStorage.GetByUserIdAsync(user.Id);
                if (products.Count < 2)
                    return RedirectToAction("Index");
                else
                    return View(products.ToViewModel());
            }
            return View((await _comparedStorage.GetByUserIdAsync(user.Id)).ToViewModel());
        }

        public async Task<IActionResult> RemoveCompareAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _comparedStorage.DeleteAsync(user.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchAsync(string serachTxt)
        {
            var products = await _productStorage.GetAllAsync();
            if (!string.IsNullOrEmpty(serachTxt)) 
                products = products.Where(p => p.Name.ToLower().Contains(serachTxt.ToLower())).ToList();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexViewModel = new HomeIndexViewModel { Products = products.ToViewModel(), CollectionsList = collections };
            return View("Index", homeIndexViewModel);
        }
        public async Task<IActionResult> FilterAsync(FiltersViewModel filters)
        {
            var products = await _productStorage.GetAllAsync();        
            var filtredProducts = await _productStorage.FiltrAsync(products, filters.ToDBModel());
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexViewModel = new HomeIndexViewModel { Products = filtredProducts.ToViewModel(), Filters = filters, CollectionsList = collections };
            return View("Index", homeIndexViewModel);
        }
        public async Task<IActionResult> CollectionAsync(string name)
        {
            var filters = new FiltersViewModel
            {
                Category = "ALL",
                StartPrice = 0,
                EndPrice = 0,
                SortBy = "Price",
                Color = "ALL",
                Size = "ALL",
                Sex = "ALL",
                MinQuantity = 0,
                MaxQuantity = 0,
            };
            var products = (await _productStorage.GetByCollectionAsync(name)).ToViewModel();
            List<CollectionViewModel> collections = (await _collectionsStorage.GetAllAsync()).Select(c => c.ToViewModel()).ToList();
            var homeIndexModel = new HomeIndexViewModel { Products = products, Filters = filters, CollectionsList = collections, IsActiveFilters = false };
            return View(homeIndexModel);
        }
    }
}
