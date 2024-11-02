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
        public IActionResult Index()
        {
            var collections = _collections.GetAll();
            var collectionsVM = collections.Select(c => c.ToViewModel()).ToList();
            return View(collectionsVM);
        }
        [HttpPost]
        public IActionResult Add(int id, string description, string name)
        {
            _collections.Add(new DB.Models.Collection { Count = 0, Name = name, Description = description });
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string name)
        {
            _collections.Delete(name);
            _products.RemoveCollectionFromProducts(name);
            return RedirectToAction("Index");
        }
        public IActionResult Products(string name)
        {
            var products = _products.GetByCollection(name);
            var productsVM = products.ToViewModel();
            return View(productsVM);
        }
        [HttpPost]
        public IActionResult Edit(string name, string newName, string description)
        {
            _collections.Edit(name, newName, description);
            return RedirectToAction("Index");
        }
        public IActionResult AddProducts(string name)
        {
            var allProducts = _products.GetAll();
            var productsInCollection = _products.GetByCollection(name);
            var productsNotInCollection = allProducts.Except(productsInCollection).ToList();
            var addProductsViewModel = new ProductsOfCollection 
            { 
                ProductsNotInCollection = productsNotInCollection.ToViewModel(), 
                ProductsInCollection = productsInCollection.ToViewModel(), Name = name 
            };
            return View(addProductsViewModel);
        }
        public IActionResult AddProduct(string idAndCollectionName)
        {
            var collectionName = idAndCollectionName.Split('*').Last();
            var productId = Guid.Parse(idAndCollectionName.Split('*').First());
             _products.AddProductToCollection(productId, collectionName);
            return RedirectToAction("AddProducts", new { name = collectionName });
        }
        public IActionResult DeleteProduct(string idAndCollectionName)
        {
            var collectionName = idAndCollectionName.Split('*').Last();
            var productId = Guid.Parse(idAndCollectionName.Split('*').First());
            _products.DeleteProductFromCollection(productId, collectionName);
            return RedirectToAction("AddProducts", new { name = collectionName });
        }
    }
}
