using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB.Models;
using VseTShirts.Models;


namespace VseTShirts.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;
        public ProductController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = Helper.ToViewModel( productsStorage.GetAll() );
            
            return View(products);
        }

        public IActionResult Delete(Guid Id)
        {
            productsStorage.Delete(Id);
             var products = Helper.ToViewModel( productsStorage.GetAll() );
            return View(nameof(Index), products);
        }

        public IActionResult QuantitiReduce(Guid id) // Уменьшение количества товара на складе
        {
            productsStorage.QuantitiReduce(id);
            return View(nameof(Index), Helper.ToViewModel( productsStorage.GetAll() ));
        }

        public IActionResult QuantityIncrease(Guid id)  //Увеличение количества товара на складе
        {
            productsStorage.QuantityIncrease(id);
            
            return View(nameof(Index), Helper.ToViewModel( productsStorage.GetAll() ));
        }

        public IActionResult Add()
        {
            return View();
        }
        public IActionResult SaveAdd(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsStorage.Add(Helper.ToDBModel(product));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            return View(Helper.ToViewModel( productsStorage.GetById(id) ));
        }

        [HttpPost]
        public ActionResult SaveСhanges(ProductViewModel newProduct)
        {
            productsStorage.EditProduct(newProduct.Id, Helper.ToDBModel( newProduct));
            return RedirectToAction(nameof(Index));
        }
    }
}
