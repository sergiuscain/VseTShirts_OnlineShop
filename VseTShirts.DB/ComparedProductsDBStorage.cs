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
        public async Task<bool> AddAsync(string userId, Product product)
        {
            var comparedProduct = new ComparedProduct { UserId = userId, product = product };
            if (_dbContext.ComparedProducts.Where(p => p.UserId == comparedProduct.UserId).Count() < 2)
            {
                await _dbContext.ComparedProducts.AddAsync(comparedProduct);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(string userId)
        {
            var products = (_dbContext.ComparedProducts.Where(p => p.UserId == userId));
            if (products.Count() > 0)
            {
                _dbContext.ComparedProducts.RemoveRange(products);
                await _dbContext.SaveChangesAsync();
                //Console.WriteLine($"Найдено {_dbContext.ComparedProducts.Where(p => p.UserId == userId).Count()} продуктов");
                return true;
            }
            return false;
        }
        public async Task<List<Product>> GetByUserIdAsync(string userId)
        {
            var comparedProducts = await _dbContext.ComparedProducts.Include(p=>p.product).ThenInclude(p => p.Images).Where(p => p.UserId == userId).ToListAsync();
            return Mapping.ToProducts(comparedProducts);
        }
    }
}