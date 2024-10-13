using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IFavoriteProductsStorage
    {
        void Add(string userId, Product product);
        List<Product> GetByUserId(string userId);
        bool IsFavorite(string userId, Product product);
        void Remove(string userId, Product product);
    }
}