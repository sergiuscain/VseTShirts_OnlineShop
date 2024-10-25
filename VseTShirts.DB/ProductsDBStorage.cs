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

    }
}   
    