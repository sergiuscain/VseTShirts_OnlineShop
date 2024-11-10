using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IFavoriteProductsStorage
    {
        Task AddAsync(string userId, Product product);
        Task<List<Product>> GetByUserIdAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, Product product);
        Task RemoveAsync(string userId, Product product);
    }
}