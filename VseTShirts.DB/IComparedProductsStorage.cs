using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IComparedProductsStorage
    {
        Task<bool> AddAsync(string userId, Product product);
        Task<bool> DeleteAsync(string userId);
        Task<List<Product>> GetByUserIdAsync(string userId);
    }
}