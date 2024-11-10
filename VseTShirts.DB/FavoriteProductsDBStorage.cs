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
        public async Task AddAsync(string userId, Product product)
        {
            var existongProduct = await _dbContext.FavoriteProducts.FirstOrDefaultAsync(p => p.Product == product && p.UserId == userId);
            if (existongProduct == null)
            {
                _dbContext.FavoriteProducts.Add(new FavoriteProduct { UserId = userId, Product = product });
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task RemoveAsync(string userId, Product product)
        {
            var existingProduct = await _dbContext.FavoriteProducts.FirstOrDefaultAsync(p => p.UserId == userId && p.Product == product);
            _dbContext.FavoriteProducts.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> IsFavoriteAsync(string userId, Product product)
        {
            return await _dbContext.FavoriteProducts.AnyAsync(p => p.UserId == userId && p.Product == product);
        }
        public async Task<List<Product>> GetByUserIdAsync(string userId)
        {
            return await _dbContext.FavoriteProducts.Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Select(p => p.Product)
                .ToListAsync();
        }
    }
}