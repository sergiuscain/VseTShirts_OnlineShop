using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseTShirts.DB;
using VseTShirts.DB.Models;
using VseTShirts.Areas.Admin.Models;
using VseTShirts.Helpers;
using VseTShirts.Models;
using VseTShirts.DB.Migrations;


namespace VseTShirts.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsStorage _productsStorage;
        private readonly ImageProvider _imageProvider;
        private readonly ICollectionsStorage _collectionsStorage;
        public ProductController(IProductsStorage productsStorage, ImageProvider imageProvider, ICollectionsStorage collectionsStorage)
        {
            _productsStorage = productsStorage;
            _imageProvider = imageProvider;
            _collectionsStorage = collectionsStorage;
        }

        public async Task<IActionResult> Index()
        {
            var products = Helper.ToViewModel( await _productsStorage.GetAllAsync() );
            
            return View(products);
        }

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            await _productsStorage.DeleteAsync(Id);
             var products = Helper.ToViewModel(await _productsStorage.GetAllAsync() );
            return View(nameof(Index), products);
        }
        public async Task<IActionResult> DeleteAllProductsAsync()
        {
            await _productsStorage.DeleteAllAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> QuantitiReduceAsync(Guid id) // Уменьшение количества товара на складе
        {
            await _productsStorage.QuantitiReduceAsync(id);
            return View(nameof(Index), Helper.ToViewModel(await _productsStorage.GetAllAsync() ));
        }

        public async Task<IActionResult> QuantityIncreaseAsync(Guid id)  //Увеличение количества товара на складе
        {
            await _productsStorage.QuantityIncreaseAsync(id);
            
            return View(nameof(Index), Helper.ToViewModel(await _productsStorage.GetAllAsync() ));
        }

        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> AddRandomProductAsync(int? count)
        {
            if (count == null || count <= 0)
            {
                count = 1;
            }
            for (int i = 0; i < count; i++)
            {
                var name = RandomData.GetName();
                var quantity = RandomData.GetQuantity();
                var price = RandomData.GetPrice();
                var sex = RandomData.GetSex();
                var category = name.Split(" ").Last();
                var color = name.Split(" ").First();
                var collectionName =  RandomData.GetCollection();
                var images = new List<ProductImage>
                {
                    new ProductImage { URL = RandomData.GetProductImagePath(sex, category ) .First() }
                };
                var randomProduct = new Product
                {
                    Name = name,
                    Quantity = quantity,
                    Price = price,
                    Category = category,
                    Color = color,
                    Size = RandomData.GetSize(),
                    Sex = sex,
                    Description = RandomData.GetDescription(),
                    Images = images,
                    NameOfCollection = collectionName
                };
                await _productsStorage.AddAsync(randomProduct);
                if ((await _collectionsStorage.GetAllAsync()).All(c => c.Name != collectionName))
                {
                    await _collectionsStorage.AddAsync(new Collection { Name = collectionName, Description = "Стандартная коллекция" });
                }
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddAsync(ProductAddViewModel product)
        {
            var imagePath = _imageProvider.SaveFiles(product.UploadedFiles, ImageFolders.Products);
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productDB = product.ToDBModel(imagePath);
            productDB.NameOfCollection = "Не задана";
            await _productsStorage.AddAsync(productDB);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            return View(Helper.ToProductEditViewModel(await _productsStorage.GetByIdAsync(id) ));
        }

        [HttpPost]
        public async Task<IActionResult> SaveСhangesAsync(ProductEditViewModel newProduct)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit" ,newProduct);
            }
            var imagePath = _imageProvider.SaveFiles(newProduct.UploadedFiles, ImageFolders.Products);
            await _productsStorage.EditProductAsync(newProduct.Id, newProduct.ToDBModel(imagePath));
            return RedirectToAction(nameof(Index));
        }
    }
}
