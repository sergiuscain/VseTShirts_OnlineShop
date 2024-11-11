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
        public async Task<IActionResult> Index(Guid id)
        {
            var product = await productStorage.GetByIdAsync(id);
            return View(product.ToViewModel());
        }
    }
}
