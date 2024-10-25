using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
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
        public IActionResult SaveAdd(ProductViewModel product)
        {
            var imagePath = _imageProvider.SaveFiles(product.UploadedFiles, ImageFolders.Products);
            product.ImagePaths = imagePath.ToArray();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productDB = product.ToDBModel();
            _productsStorage.Add(productDB);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            return View(Helper.ToViewModel( _productsStorage.GetById(id) ));
        }

        [HttpPost]
        public ActionResult SaveСhanges(ProductViewModel newProduct)
        {
            _productsStorage.EditProduct(newProduct.Id, Helper.ToDBModel( newProduct));
            return RedirectToAction(nameof(Index));
        }
    }
}
