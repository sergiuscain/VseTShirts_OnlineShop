using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IProductsStorage productStorage;
        public ProductController(IProductsStorage productsStorage)
        {
            this.productStorage = productsStorage;
        }
        public IActionResult Index(Guid id)
        {
            var product = productStorage.GetById(id);
            return View(product.ToViewModel());
        }
    }
}
