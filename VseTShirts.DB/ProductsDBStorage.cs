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


        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dbContext.Products
                .Remove(_dbContext.Products.Include(p => p.Images)
                .FirstOrDefault(p=>p.Id==Id));

            _dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var allProducts = _dbContext.Products.ToArray();
            _dbContext.Products.RemoveRange(allProducts);
            _dbContext.SaveChanges();
        }

        public void EditProduct(Guid id, Product newProduct)
        {
            _dbContext.Products
                .Remove(_dbContext.Products.Include(p => p.Images)
                .FirstOrDefault(p => p.Id == id));

            newProduct.Id = id;
            Add(newProduct);
        }


        public List<Product>? GetAll() => _dbContext.Products
            .Include(p =>p.Images)
            .Include(x=>x.CartPositions)
            .ToList();

        public Product GetById(Guid id)
        {
            return _dbContext.Products.Include(p=>p.Images).FirstOrDefault(product => product.Id == id);
        }

        public Product GetByName(string name)
        {
            return _dbContext.Products.Include(p=>p.Images).FirstOrDefault(product => product.Name == name);
        }

        public void QuantitiReduce(Guid Id)
        {
            var product = _dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == Id);
            if ( product.Quantity > 0)
                product.Quantity--;
            if (product.Quantity <= 0)
            {
                _dbContext.Products.Remove(_dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == Id));
            }
                _dbContext.SaveChanges();
        }


        public void QuantityIncrease(Guid Id)
        {
            var product = _dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == Id);
            if (product != null)
                product.Quantity++;
            _dbContext.SaveChanges();
        }
        public List<Product> Filtr(List<Product> products ,FiltersModel filters)
        {
            filters.Color = filters.Color.ToUpper();
            filters.Category = filters.Category.ToUpper();
            filters.Sex = filters.Sex.ToUpper();
            filters.Size = filters.Size.ToUpper();
            if (filters == null)
                return products;
            if(products.Count == 0) 
                return products;
            if (filters.StartPrice > 0)
            {
                if (filters.EndPrice > 0)
                {
                    products = products.Where(p => p.Price >= filters.StartPrice && p.Price<= filters.EndPrice).ToList();
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
                products = products.Where(p => p.Category == filters.Category).ToList();
            }
            if (filters.Sex == null || filters.Sex != "ALL")
            {
                products = products.Where(p => p.Sex == filters.Sex).ToList();
            }
            if (filters.Color == null || filters.Color != "ALL")
            {
                products = products.Where(p => p.Color == filters.Color).ToList();
            }
            if (filters.Size == null || filters.Size != "ALL")
            {
                products = products.Where(p => p.Size == filters.Size).ToList();
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

        public List<Product> GetByCollection(string nameOfCollection)
        {
            var products = _dbContext.Products.Where(p => p.NameOfCollection == nameOfCollection).ToList();
            return products;
        }
    }
}   
    