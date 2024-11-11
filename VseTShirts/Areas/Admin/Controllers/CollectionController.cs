using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.Areas.Admin.Models;
using VseTShirts.DB;

namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class CollectionController : Controller
    {
        private readonly ICollectionsStorage _collections;
        private readonly IProductsStorage _products;
        public CollectionController(ICollectionsStorage collections, IProductsStorage productsStorage)
        {
            _collections = collections;
            _products = productsStorage;
        }
        public async Task<IActionResult> Index()
        {
            var collections = await _collections.GetAllAsync();
            var collectionsVM = collections.Select(c => c.ToViewModel()).ToList();
            return View(collectionsVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(int id, string description, string name)
        {
            await _collections.AddAsync(new DB.Models.Collection { Count = 0, Name = name, Description = description });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(string name)
        {
            await _collections.DeleteAsync(name);
            await _products.RemoveCollectionFromProductsAsync(name);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProductsAsync(string name)
        {
            var products = await _products.GetByCollectionAsync(name);
            var productsVM = products.ToViewModel();
            return View(productsVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(string name, string newName, string description)
        {
            await _collections.EditAsync(name, newName, description);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddProductsAsync(string name)
        {
            var allProducts = await _products.GetAllAsync();
            var productsInCollection = await _products.GetByCollectionAsync(name);
            var productsNotInCollection = allProducts.Except(productsInCollection).ToList();
            var addProductsViewModel = new ProductsOfCollection 
            { 
                ProductsNotInCollection = productsNotInCollection.ToViewModel(), 
                ProductsInCollection = productsInCollection.ToViewModel(), Name = name 
            };
            return View(addProductsViewModel);
        }
        public async Task<IActionResult> AddProductAsync(string idAndCollectionName)
        {
            var collectionName = idAndCollectionName.Split('*').Last();
            var productId = Guid.Parse(idAndCollectionName.Split('*').First());
            await _products.AddProductToCollectionAsync(productId, collectionName);
            return RedirectToAction("AddProducts", new { name = collectionName });
        }
        public async Task<IActionResult> DeleteProductAsync(string idAndCollectionName)
        {
            var collectionName = idAndCollectionName.Split('*').Last();
            var productId = Guid.Parse(idAndCollectionName.Split('*').First());
            await _products.DeleteProductFromCollectionAsync(productId, collectionName);
            return RedirectToAction("AddProducts", new { name = collectionName });
        }
    }
}
