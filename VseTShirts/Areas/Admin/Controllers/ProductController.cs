using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Areas.Admin.Models;
using VseTShirts.Helpers;
using VseTShirts.Models;


namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsStorage _productsStorage;
        private readonly ImageProvider _imageProvider;
        public ProductController(IProductsStorage productsStorage, ImageProvider imageProvider)
        {
            this._productsStorage = productsStorage;
            this._imageProvider = imageProvider;
        }

        public IActionResult Index()
        {
            var products = Helper.ToViewModel( _productsStorage.GetAll() );
            
            return View(products);
        }

        public IActionResult Delete(Guid Id)
        {
            _productsStorage.Delete(Id);
             var products = Helper.ToViewModel( _productsStorage.GetAll() );
            return View(nameof(Index), products);
        }

        public IActionResult QuantitiReduce(Guid id) // Уменьшение количества товара на складе
        {
            _productsStorage.QuantitiReduce(id);
            return View(nameof(Index), Helper.ToViewModel( _productsStorage.GetAll() ));
        }

        public IActionResult QuantityIncrease(Guid id)  //Увеличение количества товара на складе
        {
            _productsStorage.QuantityIncrease(id);
            
            return View(nameof(Index), Helper.ToViewModel( _productsStorage.GetAll() ));
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveAdd(ProductAddViewModel product)
        {
            var imagePath = _imageProvider.SaveFiles(product.UploadedFiles, ImageFolders.Products);
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productDB = product.ToDBModel(imagePath);
            _productsStorage.Add(productDB);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            return View(Helper.ToProductEditViewModel( _productsStorage.GetById(id) ));
        }

        [HttpPost]
        public ActionResult SaveСhanges(ProductEditViewModel newProduct)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit" ,newProduct);
            }
            var imagePath = _imageProvider.SaveFiles(newProduct.UploadedFiles, ImageFolders.Products);
            _productsStorage.EditProduct(newProduct.Id, newProduct.ToDBModel(imagePath));
            return RedirectToAction(nameof(Index));
        }
    }
}
