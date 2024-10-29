using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class FavoriteProductsDBStorage : IFavoriteProductsStorage
    {
        private readonly DatabaseContext _dbContext;
        public FavoriteProductsDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(string userId, Product product)
        {
            var existongProduct = _dbContext.FavoriteProducts.FirstOrDefault(p => p.Product == product && p.UserId == userId);
            if (existongProduct == null)
            {
                _dbContext.FavoriteProducts.Add(new FavoriteProduct { UserId = userId, Product = product });
                _dbContext.SaveChanges();
            }
        }
        public void Remove(string userId, Product product)
        {
            var existingProduct = _dbContext.FavoriteProducts.FirstOrDefault(p => p.UserId == userId && p.Product == product);
            _dbContext.FavoriteProducts.Remove(existingProduct);
            _dbContext.SaveChanges();
        }
        public bool IsFavorite(string userId, Product product)
        {
            return _dbContext.FavoriteProducts.Any(p => p.UserId == userId && p.Product == product);
        }
        public List<Product> GetByUserId(string userId)
        {
            return _dbContext.FavoriteProducts.Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Select(p => p.Product)
                .ToList();
        }
    }
}