using Microsoft.EntityFrameworkCore;
using VseTShirts.DB;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class ProductsDBStorage : IProductsStorage
    {
        private readonly DatabaseContext _dbContext;

        public ProductsDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == Id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            var allProducts = await _dbContext.Products.ToArrayAsync();
            _dbContext.Products.RemoveRange(allProducts);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditProductAsync(Guid id, Product newProduct)
        {  
            var product = await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            _dbContext.Products.Remove(product);

            newProduct.Id = id;
            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<Product>>? GetAllAsync() => await _dbContext.Products
            .Include(p =>p.Images)
            .Include(x=>x.CartPositions)
            .ToListAsync();

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.Include(p=>p.Images).FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(product => product.Name == name);
        }

        public async Task QuantitiReduceAsync(Guid Id)
        {
            var product = await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Id);
            if ( product.Quantity > 0)
                product.Quantity--;
            if (product.Quantity <= 0)
            {
                _dbContext.Products.Remove(product);
            }
                await _dbContext.SaveChangesAsync();
        }


        public async Task QuantityIncreaseAsync(Guid Id)
        {
            var product = await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Id);
            if (product != null)
                product.Quantity++;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Product>> FiltrAsync(List<Product> products ,FiltersModel filters)
        {
            if (filters == null)
                return products;
            if(products.Count == 0) 
                return products;
            if (filters.StartPrice > 0)
            {
                if (filters.EndPrice > 0)
                {
                    products = products.Where(p => p.Price >= filters.StartPrice && p.Price <= filters.EndPrice).ToList();
                }
                else
                {
                    products = products.Where(p => p.Price >= filters.StartPrice).ToList();
                }
            }
            else if (filters.EndPrice > 0)
            {
                products = products.Where(p => p.Price <= filters.EndPrice).ToList();
            }

            if (filters.MinQuantity > 0)
            {
                if (filters.MaxQuantity > 0)
                {
                    products = products.Where(p => p.Quantity >= filters.MinQuantity && p.Quantity <= filters.MaxQuantity).ToList();
                }
                else
                {
                    products = products.Where(p => p.Quantity >= filters.MinQuantity).ToList();
                }
            }
            else if (filters.MaxQuantity > 0)
            {
                products = products.Where(p => p.Quantity <= filters.MaxQuantity).ToList();
            }
            if (filters.Category == null || filters.Category != "ALL")
            {
                products = products.Where(p => p.Category.ToUpper() == filters.Category.ToUpper()).ToList();
            }
            if (filters.Sex == null || filters.Sex != "ALL")
            {
                products = products.Where(p => p.Sex.ToUpper() == filters.Sex.ToUpper()).ToList();
            }
            if (filters.Color == null || filters.Color != "ALL")
            {
                products = products.Where(p => p.Color.ToUpper() == filters.Color.ToUpper()).ToList();
            }
            if (filters.Size == null || filters.Size != "ALL")
            {
                products = products.Where(p => p.Size.ToUpper() == filters.Size.ToUpper()).ToList();
            }
            switch (filters.SortBy)
            {
                case "Price":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "Name":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "Quantity":
                    products = products.OrderBy(p => p.Quantity).ToList();
                    break;
                case "Sex":
                    products = products.OrderBy(p => p.Sex).ToList();
                    break;
                case "Color":
                    products = products.OrderBy(p => p.Color).ToList();
                    break;
                case "Size":
                    products = products.OrderBy(p => p.Size).ToList();
                    break;
                case "Category":
                    products = products.OrderBy(p => p.Category).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
            }
                return products;
        }

        public async Task<List<Product>> GetByCollectionAsync(string nameOfCollection)
        {
            var products = await _dbContext.Products.Where(p => p.NameOfCollection == nameOfCollection)
                .Include(p => p.Images)
                .ToListAsync();
            return products;
        }

        public async Task RemoveCollectionFromProductsAsync(string name)
        {
            var products = await _dbContext.Products.Where(p => p.NameOfCollection == name).ToListAsync();
            foreach(var product in products)
            {
                product.NameOfCollection = "Не задана";
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveCollectionFromProductAsync(string name, Guid productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
                product.NameOfCollection = "Не задана";
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductFromCollectionAsync(Guid id, string collectionName)
        {
            var products = await _dbContext.Products.Where(p => p.Id == id).FirstAsync();
            products.NameOfCollection = "Не задана";
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToCollectionAsync(Guid id, string collectionName)
        {
            var products = await _dbContext.Products.Where(p => p.Id == id).FirstAsync();
            products.NameOfCollection = collectionName;
            await _dbContext.SaveChangesAsync();
        }
    }
}   
    