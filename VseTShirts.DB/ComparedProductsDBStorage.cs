using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class ComparedProductsDBStorage : IComparedProductsStorage
    {
        private readonly DatabaseContext _dbContext;
        public ComparedProductsDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(string userId, Product product)
        {
            var comparedProduct = new ComparedProduct { UserId = userId, product = product };
            if (_dbContext.ComparedProducts.Where(p => p.UserId == comparedProduct.UserId).Count() < 2)
            {
                _dbContext.ComparedProducts.Add(comparedProduct);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(string userId)
        {
            var products = (_dbContext.ComparedProducts.Where(p => p.UserId == userId));
            if (products.Count() > 0)
            {
                _dbContext.ComparedProducts.RemoveRange(products);
                _dbContext.SaveChanges();
                //Console.WriteLine($"Найдено {_dbContext.ComparedProducts.Where(p => p.UserId == userId).Count()} продуктов");
                return true;
            }
            return false;
        }
        public List<Product> GetByUserId(string userId)
        {
            var comparedProducts = _dbContext.ComparedProducts.Include(p=>p.product).Where(p => p.UserId == userId).ToList();
            return Mapping.ToProducts(comparedProducts);
        }
    }
}